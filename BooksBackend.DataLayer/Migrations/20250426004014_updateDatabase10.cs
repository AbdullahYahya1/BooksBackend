using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookFavorit_Books_BookId",
                table: "UserBookFavorit");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookFavorit_Users_UserID",
                table: "UserBookFavorit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBookFavorit",
                table: "UserBookFavorit");

            migrationBuilder.RenameTable(
                name: "UserBookFavorit",
                newName: "UserBookFavorits");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookFavorit_UserID",
                table: "UserBookFavorits",
                newName: "IX_UserBookFavorits_UserID");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserBookFavorits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBookFavorits",
                table: "UserBookFavorits",
                columns: new[] { "BookId", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookFavorits_Books_BookId",
                table: "UserBookFavorits",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookFavorits_Users_UserID",
                table: "UserBookFavorits",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookFavorits_Books_BookId",
                table: "UserBookFavorits");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookFavorits_Users_UserID",
                table: "UserBookFavorits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBookFavorits",
                table: "UserBookFavorits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBookFavorits");

            migrationBuilder.RenameTable(
                name: "UserBookFavorits",
                newName: "UserBookFavorit");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookFavorits_UserID",
                table: "UserBookFavorit",
                newName: "IX_UserBookFavorit_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBookFavorit",
                table: "UserBookFavorit",
                columns: new[] { "BookId", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookFavorit_Books_BookId",
                table: "UserBookFavorit",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookFavorit_Users_UserID",
                table: "UserBookFavorit",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
