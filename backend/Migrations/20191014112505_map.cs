using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class map : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Map");

            migrationBuilder.AddColumn<int>(
                name: "MaxX",
                table: "Map",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxY",
                table: "Map",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxX",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "MaxY",
                table: "Map");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Map",
                nullable: true);
        }
    }
}
