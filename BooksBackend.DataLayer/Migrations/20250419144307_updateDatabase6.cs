using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BookId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genre_GenreId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookUserList_UserList_UserListsId",
                table: "BookUserList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserList_Users_UserId",
                table: "UserList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReadBook_Books_BookId",
                table: "UserReadBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReadBook_Users_UserId",
                table: "UserReadBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReadBook",
                table: "UserReadBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserList",
                table: "UserList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre");

            migrationBuilder.RenameTable(
                name: "UserReadBook",
                newName: "userReadBooks");

            migrationBuilder.RenameTable(
                name: "UserList",
                newName: "userLists");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "BookGenre",
                newName: "BookGenres");

            migrationBuilder.RenameIndex(
                name: "IX_UserReadBook_UserId",
                table: "userReadBooks",
                newName: "IX_userReadBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReadBook_BookId",
                table: "userReadBooks",
                newName: "IX_userReadBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_UserList_UserId",
                table: "userLists",
                newName: "IX_userLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenreId",
                table: "BookGenres",
                newName: "IX_BookGenres_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userReadBooks",
                table: "userReadBooks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userLists",
                table: "userLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_BookId",
                table: "BookGenres",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_GenreId",
                table: "BookGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookUserList_userLists_UserListsId",
                table: "BookUserList",
                column: "UserListsId",
                principalTable: "userLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userLists_Users_UserId",
                table: "userLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userReadBooks_Books_BookId",
                table: "userReadBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userReadBooks_Users_UserId",
                table: "userReadBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_BookId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_GenreId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookUserList_userLists_UserListsId",
                table: "BookUserList");

            migrationBuilder.DropForeignKey(
                name: "FK_userLists_Users_UserId",
                table: "userLists");

            migrationBuilder.DropForeignKey(
                name: "FK_userReadBooks_Books_BookId",
                table: "userReadBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_userReadBooks_Users_UserId",
                table: "userReadBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userReadBooks",
                table: "userReadBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userLists",
                table: "userLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres");

            migrationBuilder.RenameTable(
                name: "userReadBooks",
                newName: "UserReadBook");

            migrationBuilder.RenameTable(
                name: "userLists",
                newName: "UserList");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "BookGenres",
                newName: "BookGenre");

            migrationBuilder.RenameIndex(
                name: "IX_userReadBooks_UserId",
                table: "UserReadBook",
                newName: "IX_UserReadBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userReadBooks_BookId",
                table: "UserReadBook",
                newName: "IX_UserReadBook_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_userLists_UserId",
                table: "UserList",
                newName: "IX_UserList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenre",
                newName: "IX_BookGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReadBook",
                table: "UserReadBook",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserList",
                table: "UserList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre",
                columns: new[] { "BookId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BookId",
                table: "BookGenre",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genre_GenreId",
                table: "BookGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookUserList_UserList_UserListsId",
                table: "BookUserList",
                column: "UserListsId",
                principalTable: "UserList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserList_Users_UserId",
                table: "UserList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReadBook_Books_BookId",
                table: "UserReadBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReadBook_Users_UserId",
                table: "UserReadBook",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
