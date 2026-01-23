using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenId_ExpiresAt",
                table: "RefreshTokens",
                columns: new[] { "TokenId", "ExpiresAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_TokenId_ExpiresAt",
                table: "RefreshTokens");
        }
    }
}
