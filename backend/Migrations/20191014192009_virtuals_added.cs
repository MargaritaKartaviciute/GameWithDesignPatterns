using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class virtuals_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_MapId",
                table: "Player",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Map_MapId",
                table: "Player",
                column: "MapId",
                principalTable: "Map",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Map_MapId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_MapId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Player");
        }
    }
}
