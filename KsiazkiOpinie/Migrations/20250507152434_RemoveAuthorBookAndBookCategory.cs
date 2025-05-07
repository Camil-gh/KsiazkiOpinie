using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KsiazkiOpinie.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuthorBookAndBookCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AuthorBook");
            migrationBuilder.DropTable(name: "BookCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }

    }
}
