using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addnotestoassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Assignments");
        }
    }
}
