using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VerificationCodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "VerificationCodes",
                type: "uuid",
                nullable: true);
        }
    }
}
