using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zip.WebAPI.Migrations
{
    public partial class DeleteBehavier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acount_User_UserId",
                table: "Acount");

            migrationBuilder.AddForeignKey(
                name: "FK_Acount_User_UserId",
                table: "Acount",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acount_User_UserId",
                table: "Acount");

            migrationBuilder.AddForeignKey(
                name: "FK_Acount_User_UserId",
                table: "Acount",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
