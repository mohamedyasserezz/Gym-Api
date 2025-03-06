using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class addnutrtionplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionPlans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calories_Needs = table.Column<int>(type: "int", nullable: false),
                    Carbs_Needs = table.Column<int>(type: "int", nullable: false),
                    Protein_Needs = table.Column<int>(type: "int", nullable: false),
                    Fats_Needs = table.Column<int>(type: "int", nullable: false),
                    Coach_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionPlans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NutritionPlans_Coaches_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coaches",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlans_Coach_ID",
                table: "NutritionPlans",
                column: "Coach_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionPlans");
        }
    }
}
