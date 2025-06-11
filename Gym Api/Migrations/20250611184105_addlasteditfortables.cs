using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addlasteditfortables : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Day",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEB6gf1+JtLZAjsEOqgiWzahdqGZVdMcuOKzIZfZpFaokqe3sLRj9I+mUSxMwrG+O3w==");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
