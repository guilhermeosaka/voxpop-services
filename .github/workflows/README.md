# GitHub Actions CI/CD Setup

This repository uses GitHub Actions for Continuous Integration and Continuous Deployment.

## Workflows

### 1. CI Workflow (`ci.yml`)
- **Trigger**: Pull requests to `main`
- **Purpose**: Validate code quality and ensure services build successfully
- **Smart Building**: Only builds services that have changed
- **Actions**:
  - Detects changed services using path filters
  - Restores .NET dependencies
  - Builds only affected services (`voxpop-identity` and/or `voxpop-core`)
  - Runs unit tests (if test projects exist)
  - Publishes .NET applications
- **No Docker build**: Docker images are built once in CD workflow
- **No deployment**: Only validates that code is ready to merge

**How it works:**
- Changes to `Voxpop.Identity/**` → Builds Identity only
- Changes to `Voxpop.Core/**` → Builds Core only
- Changes to `Voxpop.Packages/**` → Builds ALL services (shared dependency)
- Changes to docs only → Skips build entirely

### 2. CD Workflow (`cd.yml`)
- **Trigger**: Push to `main` branch (after PR merge)
- **Purpose**: Deploy services to AWS ECS
- **Actions**: 
  - Builds Docker images
  - Pushes to Amazon ECR
  - Updates ECS task definitions with secrets
  - Deploys to ECS cluster
- **Smart deployment**: Only deploys services that have changed

### Workflow Philosophy: Build Once

**Why CI doesn't build Docker images:**
- Avoids duplicate builds (faster feedback)
- CD builds the Docker image once and deploys it
- CI validates .NET code compiles and tests pass
- Dockerfile validation happens in CD (rare to change)

**Direct pushes to main:**
- CD workflow runs automatically (no CI)
- Use for hotfixes or when working solo
- For team collaboration, use PRs to get CI validation

## Architecture

### Service Registry (`.github/services.json`)
All service configurations are centralized in a single JSON file:

```json
{
  "services": [
    {
      "name": "identity",
      "path": "Voxpop.Identity",
      "project": "Voxpop.Identity/src/Voxpop.Identity.Api/Voxpop.Identity.Api.csproj",
      "dockerfile": "Voxpop.Identity/src/Voxpop.Identity.Api/Dockerfile"
    }
  ]
}
```

**To add a new service:**
1. Add entry to `services.json`
2. Add path filter in `ci.yml` and `cd.yml`
3. That's it! No bash scripting required.

### Reusable Components

#### `.github/actions/dotnet-build-test`
Composite action that encapsulates:
- .NET SDK setup
- Dependency restoration
- Build
- Test discovery and execution
- Publish

**Benefits**: Consistent build logic across CI/CD workflows.

## Required GitHub Secrets

The CD workflow uses **environment-specific secrets** from the `beta` GitHub environment.

### How to Add Secrets

1. Go to https://github.com/guilhermeosaka/voxpop/settings/environments
2. Click on the **`beta`** environment (or create it if it doesn't exist)
3. Under "Environment secrets", add the following:

### AWS_ROLE_ARN
- **Name**: `AWS_ROLE_ARN`
- **Value**: Get this from your Terraform output:
  ```bash
  cd voxpop.infra/envs/beta
  terraform output -raw github_actions_role_arn
  ```
- **Purpose**: Allows GitHub Actions to authenticate with AWS using OIDC (no long-lived credentials needed)

> **Note**: Database credentials are managed by Terraform and stored in AWS Secrets Manager. They are automatically injected into ECS tasks. You do NOT need to add them as GitHub secrets.

## How It Works

### On Pull Request:
1. Developer creates a PR
2. CI workflow detects changed services
3. Only affected services are built to verify code compiles
4. PR shows build status ✅ or ❌
5. No deployment happens

### On Merge to Main:
1. PR is merged to `main`
2. CD workflow runs automatically
3. Detects which services changed
4. Builds and pushes Docker images to ECR
5. Downloads current ECS task definition (which has Terraform-managed secrets from AWS Secrets Manager)
6. Updates task definition with new Docker image
7. Deploys to ECS cluster
8. Waits for service stability

## Deployment Architecture

```
┌─────────────────┐
│   GitHub PR     │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  CI Workflow    │  ← Builds changed services only
│  (ci.yml)       │  ← No deployment
└─────────────────┘

         │ Merge to main
         ▼
┌─────────────────┐
│  CD Workflow    │  ← Builds changed services
│  (cd.yml)       │  ← Pushes to ECR
└────────┬────────┘  ← Deploys to ECS
         │
         ▼
┌─────────────────────────────────┐
│  AWS ECS Cluster: voxpop-beta   │
│  ┌─────────────────────────┐   │
│  │ voxpop-identity-beta    │   │
│  │ - Port 8080             │   │
│  │ - /identity/* routes    │   │
│  └─────────────────────────┘   │
│  ┌─────────────────────────┐   │
│  │ voxpop-core-beta        │   │
│  │ - Port 8080             │   │
│  │ - /core/* routes        │   │
│  └─────────────────────────┘   │
└─────────────────────────────────┘
```

## ECR Repositories

The workflows automatically create these ECR repositories if they don't exist:
- `voxpop-identity` - Identity service images
- `voxpop-core` - Core service images

Images are tagged with the Git commit SHA for traceability.

## Monitoring Deployments

### View Workflow Runs
- Go to https://github.com/guilhermeosaka/voxpop/actions
- Click on a workflow run to see details
- Each service builds in parallel (matrix strategy)

### Check ECS Deployment
```bash
# Check service status
aws ecs describe-services \
  --cluster voxpop-beta \
  --services voxpop-identity-beta voxpop-core-beta \
  --region us-east-1

# View running tasks
aws ecs list-tasks \
  --cluster voxpop-beta \
  --service-name voxpop-identity-beta \
  --region us-east-1
```

### View Logs
```bash
# Identity service logs
aws logs tail /ecs/voxpop-identity-beta --follow --region us-east-1

# Core service logs
aws logs tail /ecs/voxpop-core-beta --follow --region us-east-1
```

## Troubleshooting

### Build Fails
- Check the GitHub Actions logs for error details
- Verify Dockerfile paths are correct in `services.json`
- Ensure all dependencies are available

### Deployment Fails
- Verify `AWS_ROLE_ARN` secret is set correctly in GitHub
- Check AWS permissions for the OIDC role
- Verify ECS cluster and services exist
- Check CloudWatch logs for container errors

### Service Unhealthy After Deployment
- Check CloudWatch logs for application errors
- Verify database credentials in Terraform (`voxpop.infra/envs/beta/terraform.tfvars`)
- Check security groups allow traffic
- Verify environment variables are set correctly in ECS task definition

## Rollback

If a deployment causes issues:

```bash
# Get previous task definition revision
aws ecs describe-services \
  --cluster voxpop-beta \
  --service voxpop-identity-beta \
  --region us-east-1

# Rollback to previous revision
aws ecs update-service \
  --cluster voxpop-beta \
  --service voxpop-identity-beta \
  --task-definition voxpop-identity-beta:<previous-revision> \
  --region us-east-1
```

## Next Steps

1. ✅ Add the `AWS_ROLE_ARN` secret to GitHub (get value from Terraform output)
2. ✅ Create a test PR to verify CI workflow
3. ✅ Merge PR to verify CD workflow
4. ✅ Monitor deployment in AWS console
5. ✅ Test service endpoints

## Security Notes

- Never commit secrets to the repository
- Database credentials are managed in Terraform (not in GitHub)
- GitHub only needs `AWS_ROLE_ARN` for OIDC authentication (no long-lived AWS credentials)
- Consider migrating to AWS Secrets Manager for production environments
- Rotate passwords regularly in Terraform and redeploy
