using System;
using System.Collections.Generic;
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
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    EventsIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "choices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    BonusType = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsequencesIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choices", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChoicesIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    EventsIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
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
                    EventsIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
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
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_structures", x => x.Id);
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
                name: "IX_events_Name",
                table: "events",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_events_ReferenceId",
                table: "events",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_events_Type",
                table: "events",
                column: "Type");

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
                name: "IX_structures_ReferenceId",
                table: "structures",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_structures_Type",
                table: "structures",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaigns");

            migrationBuilder.DropTable(
                name: "choices");

            migrationBuilder.DropTable(
                name: "consequences");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "races");

            migrationBuilder.DropTable(
                name: "structures");
        }
    }
}
