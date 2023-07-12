using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameManager.WebApp.BS.Repository.Migrations
{
    public partial class second_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DispleyName",
                table: "GameCollection",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "DispleyIndex",
                table: "GameCollection",
                newName: "DisplayIndex");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Game",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "GameCollection",
                newName: "DispleyName");

            migrationBuilder.RenameColumn(
                name: "DisplayIndex",
                table: "GameCollection",
                newName: "DispleyIndex");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Game",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_UserCategory_UserCategoryId",
                        column: x => x.UserCategoryId,
                        principalTable: "UserCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserCategoryId",
                table: "Role",
                column: "UserCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }
    }
}
