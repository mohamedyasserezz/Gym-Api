using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addeditforremoetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJSsJFj0PQu7AZbjUWHSk0w3kYv54njartoA69TSPkWt/7+BJrYMohS4vwx3hw1f3g==");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments",
                column: "Exercise_ID",
                principalTable: "Exercises",
                principalColumn: "Exercise_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEK8FWqSorWcgZsHQ5JBBhw2BHgrylvDQRKDnp/FXkc1DjUbPgW+6U5GCw1ejOXDEuw==");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments",
                column: "Exercise_ID",
                principalTable: "Exercises",
                principalColumn: "Exercise_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
