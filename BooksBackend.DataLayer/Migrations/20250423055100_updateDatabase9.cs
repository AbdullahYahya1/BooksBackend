using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBookFavorit",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookFavorit", x => new { x.BookId, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserBookFavorit_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookFavorit_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBookFavorit_UserID",
                table: "UserBookFavorit",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookFavorit");
        }
    }
}
