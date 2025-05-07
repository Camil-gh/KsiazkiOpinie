using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KsiazkiOpinie.Migrations
{
    /// <inheritdoc />
    public partial class _22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_books_Authors_AuthorsId",
                table: "authors_books");

            migrationBuilder.DropForeignKey(
                name: "FK_authors_books_Books_BooksId",
                table: "authors_books");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_books_Books_BooksId",
                table: "categories_books");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_books_Categories_CategoriesId",
                table: "categories_books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories_books",
                table: "categories_books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors_books",
                table: "authors_books");

            migrationBuilder.RenameTable(
                name: "categories_books",
                newName: "BookCategory");

            migrationBuilder.RenameTable(
                name: "authors_books",
                newName: "AuthorBook");

            migrationBuilder.RenameIndex(
                name: "IX_categories_books_CategoriesId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_authors_books_BooksId",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BooksId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCategory",
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBooks",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBooks", x => new { x.CategoryId, x.BookId });
                    table.ForeignKey(
                        name: "FK_CategoryBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryBooks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBooks_BookId",
                table: "CategoryBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory");

            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "CategoryBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCategory",
                table: "BookCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook");

            migrationBuilder.RenameTable(
                name: "BookCategory",
                newName: "categories_books");

            migrationBuilder.RenameTable(
                name: "AuthorBook",
                newName: "authors_books");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriesId",
                table: "categories_books",
                newName: "IX_categories_books_CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BooksId",
                table: "authors_books",
                newName: "IX_authors_books_BooksId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories_books",
                table: "categories_books",
                columns: new[] { "BooksId", "CategoriesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors_books",
                table: "authors_books",
                columns: new[] { "AuthorsId", "BooksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_authors_books_Authors_AuthorsId",
                table: "authors_books",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_authors_books_Books_BooksId",
                table: "authors_books",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_books_Books_BooksId",
                table: "categories_books",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_books_Categories_CategoriesId",
                table: "categories_books",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
