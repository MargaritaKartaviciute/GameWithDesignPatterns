using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class movesCountIsDeadAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Player",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MovesCount",
                table: "Player",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "MovesCount",
                table: "Player");
        }
    }
}
