using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class ItemPowerTypeForSpecificTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PowerFor",
                table: "Item",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PowerFor",
                table: "Item");
        }
    }
}
