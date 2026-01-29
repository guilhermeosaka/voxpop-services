using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxpop.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "continent_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_continent_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "continents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_continents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    continent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "education_level_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_level_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "education_levels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_levels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ethnicities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ethnicities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ethnicity_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ethnicity_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "gender_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "genders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "occupation_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occupation_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "occupations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occupations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "polls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    archived_by = table.Column<Guid>(type: "uuid", nullable: true),
                    archived_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "race_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_race_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "races",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_races", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "religion_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_religion_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "religions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_religions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "state_translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state_translations", x => new { x.id, x.language });
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "poll_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    poll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poll_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_poll_options_polls_poll_id",
                        column: x => x.poll_id,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    gender_id = table.Column<Guid>(type: "uuid", nullable: true),
                    city_id = table.Column<Guid>(type: "uuid", nullable: true),
                    state_id = table.Column<Guid>(type: "uuid", nullable: true),
                    country_id = table.Column<Guid>(type: "uuid", nullable: true),
                    education_level_id = table.Column<Guid>(type: "uuid", nullable: true),
                    occupation_id = table.Column<Guid>(type: "uuid", nullable: true),
                    religion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    race_id = table.Column<Guid>(type: "uuid", nullable: true),
                    ethnicity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    archived_by = table.Column<Guid>(type: "uuid", nullable: true),
                    archived_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_profiles_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_education_levels_education_level_id",
                        column: x => x.education_level_id,
                        principalTable: "education_levels",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_ethnicities_ethnicity_id",
                        column: x => x.ethnicity_id,
                        principalTable: "ethnicities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_genders_gender_id",
                        column: x => x.gender_id,
                        principalTable: "genders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_occupations_occupation_id",
                        column: x => x.occupation_id,
                        principalTable: "occupations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_races_race_id",
                        column: x => x.race_id,
                        principalTable: "races",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_religions_religion_id",
                        column: x => x.religion_id,
                        principalTable: "religions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_profiles_states_state_id",
                        column: x => x.state_id,
                        principalTable: "states",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cities_code_state_id",
                table: "cities",
                columns: new[] { "code", "state_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_continents_code",
                table: "continents",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_code",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_education_levels_code",
                table: "education_levels",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ethnicities_code",
                table: "ethnicities",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genders_code",
                table: "genders",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_occupations_code",
                table: "occupations",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_poll_options_poll_id_value",
                table: "poll_options",
                columns: new[] { "poll_id", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_races_code",
                table: "races",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_religions_code",
                table: "religions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_states_code_country_id",
                table: "states",
                columns: new[] { "code", "country_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_city_id",
                table: "user_profiles",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_country_id",
                table: "user_profiles",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_education_level_id",
                table: "user_profiles",
                column: "education_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_ethnicity_id",
                table: "user_profiles",
                column: "ethnicity_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_gender_id",
                table: "user_profiles",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_occupation_id",
                table: "user_profiles",
                column: "occupation_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_race_id",
                table: "user_profiles",
                column: "race_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_religion_id",
                table: "user_profiles",
                column: "religion_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_state_id",
                table: "user_profiles",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_translations");

            migrationBuilder.DropTable(
                name: "continent_translations");

            migrationBuilder.DropTable(
                name: "continents");

            migrationBuilder.DropTable(
                name: "country_translations");

            migrationBuilder.DropTable(
                name: "education_level_translations");

            migrationBuilder.DropTable(
                name: "ethnicity_translations");

            migrationBuilder.DropTable(
                name: "gender_translations");

            migrationBuilder.DropTable(
                name: "occupation_translations");

            migrationBuilder.DropTable(
                name: "poll_options");

            migrationBuilder.DropTable(
                name: "race_translations");

            migrationBuilder.DropTable(
                name: "religion_translations");

            migrationBuilder.DropTable(
                name: "state_translations");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "polls");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "education_levels");

            migrationBuilder.DropTable(
                name: "ethnicities");

            migrationBuilder.DropTable(
                name: "genders");

            migrationBuilder.DropTable(
                name: "occupations");

            migrationBuilder.DropTable(
                name: "races");

            migrationBuilder.DropTable(
                name: "religions");

            migrationBuilder.DropTable(
                name: "states");
        }
    }
}
