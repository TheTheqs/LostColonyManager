using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostColonyManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "races",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    traits_culture = table.Column<int>(type: "integer", nullable: false),
                    traits_diplomacy = table.Column<int>(type: "integer", nullable: false),
                    traits_power = table.Column<int>(type: "integer", nullable: false),
                    traits_technology = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "structures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    BonusType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Cost = table.Column<string>(type: "jsonb", nullable: false),
                    Requeriments = table.Column<string>(type: "jsonb", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_structures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: true),
                    RaceId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlanetId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.CheckConstraint("CK_events_exactly_one_owner", "(CASE WHEN \"CampaignId\" IS NULL THEN 0 ELSE 1 END\r\n                     + CASE WHEN \"RaceId\"     IS NULL THEN 0 ELSE 1 END\r\n                     + CASE WHEN \"PlanetId\"   IS NULL THEN 0 ELSE 1 END) = 1");
                    table.ForeignKey(
                        name: "FK_events_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planet_structures",
                columns: table => new
                {
                    PlanetId = table.Column<Guid>(type: "uuid", nullable: false),
                    StructureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planet_structures", x => new { x.PlanetId, x.StructureId });
                    table.ForeignKey(
                        name: "FK_planet_structures_planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planet_structures_structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "choices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    BonusType = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_choices_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consequences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    MinRange = table.Column<int>(type: "integer", nullable: false),
                    MaxRange = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Target = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    ChoiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consequences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_consequences_choices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campaigns_Name",
                table: "campaigns",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_choices_EventId",
                table: "choices",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_choices_Name",
                table: "choices",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_consequences_ChoiceId",
                table: "consequences",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_consequences_Name",
                table: "consequences",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_events_CampaignId",
                table: "events",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_events_Name",
                table: "events",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_events_PlanetId",
                table: "events",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_events_RaceId",
                table: "events",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_events_Type",
                table: "events",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_planet_structures_PlanetId",
                table: "planet_structures",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_planet_structures_StructureId",
                table: "planet_structures",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_planets_Category",
                table: "planets",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_planets_Name",
                table: "planets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_races_Name",
                table: "races",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_structures_Name",
                table: "structures",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_structures_Type",
                table: "structures",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consequences");

            migrationBuilder.DropTable(
                name: "planet_structures");

            migrationBuilder.DropTable(
                name: "choices");

            migrationBuilder.DropTable(
                name: "structures");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "campaigns");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "races");
        }
    }
}
