using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodeHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "VerificationCodes",
                newName: "CodeHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeHash",
                table: "VerificationCodes",
                newName: "Code");
        }
    }
}
