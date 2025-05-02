using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeNewAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmedByAdmin",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForgetPasswordOtpConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                columns: new[] { "IsForgetPasswordOtpConfirmed", "PasswordHash" },
                values: new object[] { false, "AQAAAAIAAYagAAAAEPC5iBOJ4jqR6vP2tNGkpX9uULDMEVi9iGxo2y0Ut7ii7stEe1CQkBSL8kyiOYUjpg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmedByAdmin",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "IsForgetPasswordOtpConfirmed",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEO8krX6B0YDB8owIsv8aq7JJB3Le+kx94IGgCUyh5v+Nmt++f3HfQIVG2wbMvF+nFw==");
        }
    }
}
