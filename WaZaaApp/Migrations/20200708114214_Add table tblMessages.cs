using Microsoft.EntityFrameworkCore.Migrations;

namespace WaZaaApp.Migrations
{
    public partial class AddtabletblMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 4000, nullable: false),
                    ChatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMessages_tblChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "tblChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblMessages_tblUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMessages_ChatId",
                table: "tblMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMessages_UserId",
                table: "tblMessages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMessages");
        }
    }
}
