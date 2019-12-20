using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class mapObjFKdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCutItem_MapObject_MapObjectId",
                table: "PlayerCutItem");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCutItem_MapObjectId",
                table: "PlayerCutItem");

            migrationBuilder.DropColumn(
                name: "MapObjectId",
                table: "PlayerCutItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapObjectId",
                table: "PlayerCutItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCutItem_MapObjectId",
                table: "PlayerCutItem",
                column: "MapObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCutItem_MapObject_MapObjectId",
                table: "PlayerCutItem",
                column: "MapObjectId",
                principalTable: "MapObject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
