using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class editassignmentandplandata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CompleteRate",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Snack",
                table: "NutritionPlans",
                newName: "ThirdMeal");

            migrationBuilder.RenameColumn(
                name: "Lunch",
                table: "NutritionPlans",
                newName: "Snacks");

            migrationBuilder.RenameColumn(
                name: "Dinner",
                table: "NutritionPlans",
                newName: "SecondMeal");

            migrationBuilder.RenameColumn(
                name: "Breakfast",
                table: "NutritionPlans",
                newName: "FirstMeal");

            migrationBuilder.AddColumn<string>(
                name: "FifthMeal",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FourthMeal",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vitamins",
                table: "NutritionPlans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FifthMeal",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "FourthMeal",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "Vitamins",
                table: "NutritionPlans");

            migrationBuilder.RenameColumn(
                name: "ThirdMeal",
                table: "NutritionPlans",
                newName: "Snack");

            migrationBuilder.RenameColumn(
                name: "Snacks",
                table: "NutritionPlans",
                newName: "Lunch");

            migrationBuilder.RenameColumn(
                name: "SecondMeal",
                table: "NutritionPlans",
                newName: "Dinner");

            migrationBuilder.RenameColumn(
                name: "FirstMeal",
                table: "NutritionPlans",
                newName: "Breakfast");

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CompleteRate",
                table: "Assignments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
