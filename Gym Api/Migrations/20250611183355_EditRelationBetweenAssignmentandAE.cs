using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class EditRelationBetweenAssignmentandAE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignmentExercises_Assignments_AssignmentId",
                table: "assignmentExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_assignmentExercises_Exercises_Exercise_ID",
                table: "assignmentExercises");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDkJR9ZCB8pjBSC3M0ScwQYXkr6NiZdVWGBo0/cuyVIMWuPN/TKHHhOro74R0aI2Mg==");

            migrationBuilder.AddForeignKey(
                name: "FK_assignmentExercises_Assignments_AssignmentId",
                table: "assignmentExercises",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Assignment_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_assignmentExercises_Exercises_Exercise_ID",
                table: "assignmentExercises",
                column: "Exercise_ID",
                principalTable: "Exercises",
                principalColumn: "Exercise_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignmentExercises_Assignments_AssignmentId",
                table: "assignmentExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_assignmentExercises_Exercises_Exercise_ID",
                table: "assignmentExercises");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMh2D01mGDOcR/z2c/FHChZlUwZ0/Ne2il0dq+pgQZI7LgFjnQmqFAcbDn3AsOMNiQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_assignmentExercises_Assignments_AssignmentId",
                table: "assignmentExercises",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Assignment_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_assignmentExercises_Exercises_Exercise_ID",
                table: "assignmentExercises",
                column: "Exercise_ID",
                principalTable: "Exercises",
                principalColumn: "Exercise_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
