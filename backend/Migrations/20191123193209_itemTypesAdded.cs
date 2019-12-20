using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class itemTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PowerFor",
                table: "Item",
                newName: "ItemType");

            migrationBuilder.AddColumn<string>(
                name: "ItemPhotoSrc",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemSubType",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPhotoSrc",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemSubType",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Item",
                newName: "PowerFor");
        }
    }
}
