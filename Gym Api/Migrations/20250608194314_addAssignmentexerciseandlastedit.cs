using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addAssignmentexerciseandlastedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Categories_Category_ID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_Coaches_Coach_ID",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Coaches_Coach_ID",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_User_ID",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_Exercise_ID",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Exercise_ID",
                table: "Assignments");

            migrationBuilder.CreateTable(
                name: "assignmentExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exercise_ID = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignmentExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignmentExercises_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Assignment_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assignmentExercises_Exercises_Exercise_ID",
                        column: x => x.Exercise_ID,
                        principalTable: "Exercises",
                        principalColumn: "Exercise_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDhGBiQXEq3llwpxMZu2D7oI64vWsgTF0YxXm62u9SN8/pDJj1gO+k5Za15XXm8lyw==");

            migrationBuilder.CreateIndex(
                name: "IX_assignmentExercises_AssignmentId",
                table: "assignmentExercises",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_assignmentExercises_Exercise_ID",
                table: "assignmentExercises",
                column: "Exercise_ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Categories_Category_ID",
                table: "Exercises",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_Coaches_Coach_ID",
                table: "NutritionPlans",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Coaches_Coach_ID",
                table: "Subscriptions",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_User_ID",
                table: "Subscriptions",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Categories_Category_ID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_Coaches_Coach_ID",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Coaches_Coach_ID",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_User_ID",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "assignmentExercises");

            migrationBuilder.AddColumn<int>(
                name: "Exercise_ID",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEM+lFlGv57XdJ/RcC7AlNv1B42LI2qiELGBBWxImXrQbppT3SalfP/iDWMMrxkdtbQ==");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Exercise_ID",
                table: "Assignments",
                column: "Exercise_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Exercises_Exercise_ID",
                table: "Assignments",
                column: "Exercise_ID",
                principalTable: "Exercises",
                principalColumn: "Exercise_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Categories_Category_ID",
                table: "Exercises",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_Coaches_Coach_ID",
                table: "NutritionPlans",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_Users_User_ID",
                table: "NutritionPlans",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_User_ID",
                table: "Orders",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Coaches_Coach_ID",
                table: "Subscriptions",
                column: "Coach_ID",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_User_ID",
                table: "Subscriptions",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
