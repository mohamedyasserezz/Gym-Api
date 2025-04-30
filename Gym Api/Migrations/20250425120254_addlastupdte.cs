using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addlastupdte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Assignment_Date",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lname",
                table: "Coaches",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Fname",
                table: "Coaches",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalConditions",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProof",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Breakfast",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dinner",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lunch",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Snack",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MedicalConditions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "message",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentProof",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Dinner",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Lunch",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Snack",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Lname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Coaches",
                newName: "lname");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Coaches",
                newName: "Fname");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Ratings",
                table: "Coaches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Assignment_Date",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
