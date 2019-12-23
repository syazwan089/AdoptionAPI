using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptionApi.Migrations
{
    public partial class fixRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdopItem_Users_UsersId",
                table: "AdopItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdopItem");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "AdopItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdopItem_Users_UsersId",
                table: "AdopItem",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdopItem_Users_UsersId",
                table: "AdopItem");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "AdopItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdopItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AdopItem_Users_UsersId",
                table: "AdopItem",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
