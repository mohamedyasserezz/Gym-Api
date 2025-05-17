using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class editrelbetusernandplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEM+lFlGv57XdJ/RcC7AlNv1B42LI2qiELGBBWxImXrQbppT3SalfP/iDWMMrxkdtbQ==");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJSsJFj0PQu7AZbjUWHSk0w3kYv54njartoA69TSPkWt/7+BJrYMohS4vwx3hw1f3g==");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlans_User_ID",
                table: "NutritionPlans",
                column: "User_ID",
                unique: true);
        }
    }
}
