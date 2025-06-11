using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Api.Migrations
{
    /// <inheritdoc />
    public partial class EditAssignmentConfiguratio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMh2D01mGDOcR/z2c/FHChZlUwZ0/Ne2il0dq+pgQZI7LgFjnQmqFAcbDn3AsOMNiQ==");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Coaches_Coach_ID",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_User_ID",
                table: "Assignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6dc6528a-b280-4770-9eae-82671ee81ef7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKlr2ftcptt+ShnODhfjQqAmiKFmngAdr8OmXLhykX0xIX7kAv+yBVBd5oNSIMNk7A==");

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
    }
}
