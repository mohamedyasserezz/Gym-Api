using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class updateexercisedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Video_URL",
                table: "Exercises",
                newName: "Image_url");

            migrationBuilder.AddColumn<string>(
                name: "Image_gif",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_gif",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Image_url",
                table: "Exercises",
                newName: "Video_URL");
        }
    }
}
