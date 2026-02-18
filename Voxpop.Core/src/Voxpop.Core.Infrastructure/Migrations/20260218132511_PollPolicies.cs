using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PollPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_votes_poll_id",
                table: "votes");

            migrationBuilder.AddColumn<Guid>(
                name: "slot_index",
                table: "votes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "vote_mode",
                table: "polls",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "access",
                table: "polls",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "results_access",
                table: "polls",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "results_visibility",
                table: "polls",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_votes_poll_id_user_id_slot_index",
                table: "votes",
                columns: new[] { "poll_id", "user_id", "slot_index" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_votes_poll_id_user_id_slot_index",
                table: "votes");

            migrationBuilder.DropColumn(
                name: "slot_index",
                table: "votes");

            migrationBuilder.DropColumn(
                name: "access",
                table: "polls");

            migrationBuilder.DropColumn(
                name: "results_access",
                table: "polls");

            migrationBuilder.DropColumn(
                name: "results_visibility",
                table: "polls");

            migrationBuilder.AlterColumn<int>(
                name: "vote_mode",
                table: "polls",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_votes_poll_id",
                table: "votes",
                column: "poll_id");
        }
    }
}
