using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class nutrtionplananduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_ID",
                table: "NutritionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans",
                column: "User_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans");

            migrationBuilder.DropIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "NutritionPlans");
        }
    }
}
