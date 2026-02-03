using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _0003_UpdatesRefreshTokenIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_token_id_expires_at",
                table: "refresh_tokens");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_token_id",
                table: "refresh_tokens",
                column: "token_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_token_id",
                table: "refresh_tokens");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_token_id_expires_at",
                table: "refresh_tokens",
                columns: new[] { "token_id", "expires_at" });
        }
    }
}
