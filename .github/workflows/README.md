# GitHub Actions CI/CD Setup

This repository uses GitHub Actions for Continuous Integration and Continuous Deployment.

## Workflows

### 1. CI Workflow (`ci.yml`)
- **Trigger**: Pull requests to `main`
- **Purpose**: Build and validate Docker images for both services
- **Actions**: Builds both `voxpop-identity` and `voxpop-core` services
- **No deployment**: Only validates that code builds successfully

### 2. CD Workflow (`cd.yml`)
- **Trigger**: Push to `main` branch (after PR merge)
- **Purpose**: Deploy services to AWS ECS
- **Actions**: 
  - Builds Docker images
  - Pushes to Amazon ECR
  - Updates ECS task definitions with secrets
  - Deploys to ECS cluster
- **Smart deployment**: Only deploys services that have changed

## Required GitHub Secrets

Before the workflows can run, you need to add these secrets to your repository:

### How to Add Secrets

1. Go to https://github.com/guilhermeosaka/voxpop/settings/secrets/actions
2. Click **"New repository secret"**
3. Add each of the following secrets:

### Secret 1: AWS_ROLE_ARN
- **Name**: `AWS_ROLE_ARN`
- **Value**: `arn:aws:iam::303155796105:role/voxpop-beta-github-actions-role`
- **Purpose**: Allows GitHub Actions to authenticate with AWS using OIDC

### Secret 2: DB_PASSWORD
- **Name**: `DB_PASSWORD`
- **Value**: Your RDS database password (same as used in Terraform)
- **Purpose**: Injected into ECS containers for database authentication
- **⚠️ Important**: This must match the password you used when deploying infrastructure with Terraform

### Secret 3: RABBITMQ_PASSWORD
- **Name**: `RABBITMQ_PASSWORD`
- **Value**: Your RabbitMQ password
- **Purpose**: Injected into ECS containers for message broker authentication
- **Note**: Only required if RabbitMQ is enabled in your infrastructure

## How It Works

### On Pull Request:
1. Developer creates a PR
2. CI workflow runs automatically
3. Both services are built to verify code compiles
4. PR shows build status ✅ or ❌
5. No deployment happens

### On Merge to Main:
1. PR is merged to `main`
2. CD workflow runs automatically
3. Detects which services changed
4. Builds and pushes Docker images to ECR
5. Updates ECS task definitions with:
   - New Docker image
   - Secrets (DB_PASSWORD, RABBITMQ_PASSWORD)
   - Environment variables from Terraform
6. Deploys to ECS cluster
7. Waits for service stability

## Deployment Architecture

```
┌─────────────────┐
│   GitHub PR     │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  CI Workflow    │  ← Builds both services
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
- Verify Dockerfile paths are correct
- Ensure all dependencies are available

### Deployment Fails
- Verify all three secrets are set correctly
- Check AWS permissions for the OIDC role
- Verify ECS cluster and services exist
- Check CloudWatch logs for container errors

### Service Unhealthy After Deployment
- Check CloudWatch logs for application errors
- Verify database connection (DB_PASSWORD is correct)
- Verify RabbitMQ connection (if enabled)
- Check security groups allow traffic

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

1. ✅ Add the three required secrets to GitHub
2. ✅ Create a test PR to verify CI workflow
3. ✅ Merge PR to verify CD workflow
4. ✅ Monitor deployment in AWS console
5. ✅ Test service endpoints

## Security Notes

- Never commit secrets to the repository
- Secrets are encrypted in GitHub
- Secrets are injected at deployment time
- Consider migrating to AWS Secrets Manager for production
- Rotate passwords regularly
