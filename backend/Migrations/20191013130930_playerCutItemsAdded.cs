using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class playerCutItemsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerCutItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoinsWorth = table.Column<int>(nullable: false),
                    MapObjectId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCutItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCutItem_MapObject_MapObjectId",
                        column: x => x.MapObjectId,
                        principalTable: "MapObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCutItem_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCutItem_MapObjectId",
                table: "PlayerCutItem",
                column: "MapObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCutItem_PlayerId",
                table: "PlayerCutItem",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerCutItem");

            migrationBuilder.AddColumn<int>(
                name: "CoinsWorthAmount",
                table: "MapObject",
                nullable: false,
                defaultValue: 0);
        }
    }
}
