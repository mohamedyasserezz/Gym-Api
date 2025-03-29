using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class editcategorydescrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "Coaches",
                newName: "lname");

            migrationBuilder.RenameColumn(
                name: "Category_Description",
                table: "Categories",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lname",
                table: "Coaches",
                newName: "Lname");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "Category_Description");
        }
    }
}
