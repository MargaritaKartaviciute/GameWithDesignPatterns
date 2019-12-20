using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class added_virtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rock");

            migrationBuilder.DropTable(
                name: "Tree");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Water",
                table: "Water");

            migrationBuilder.RenameTable(
                name: "Water",
                newName: "MapObject");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MapObject",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapObject",
                table: "MapObject",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MapObject_MapId",
                table: "MapObject",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapObject_Map_MapId",
                table: "MapObject",
                column: "MapId",
                principalTable: "Map",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapObject_Map_MapId",
                table: "MapObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapObject",
                table: "MapObject");

            migrationBuilder.DropIndex(
                name: "IX_MapObject_MapId",
                table: "MapObject");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MapObject");

            migrationBuilder.RenameTable(
                name: "MapObject",
                newName: "Water");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Water",
                table: "Water",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Rock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MapId = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tree",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MapId = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tree", x => x.Id);
                });
        }
    }
}
