using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Products",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Orders",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Categories",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Brands",
                newName: "CreationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Products",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Orders",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Categories",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Brands",
                newName: "DataCriacao");
        }
    }
}
