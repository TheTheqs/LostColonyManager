using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostColonyManager.Migrations
{
    /// <inheritdoc />
    public partial class AddBonusTargetAndIncreasePerTurnToStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "BonusTargets",
                table: "structures",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IncreasePerTurn",
                table: "structures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusTargets",
                table: "structures");

            migrationBuilder.DropColumn(
                name: "IncreasePerTurn",
                table: "structures");
        }
    }
}
