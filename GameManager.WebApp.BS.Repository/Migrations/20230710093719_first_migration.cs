using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameManager.WebApp.BS.Repository.Migrations
{
    public partial class first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DispleyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispleyIndex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayIndex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDateOfGame = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_GameCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "GameCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSubCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSubCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSubCollection_GameCollection_GameCollectionId",
                        column: x => x.GameCollectionId,
                        principalTable: "GameCollection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCategoryId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IUserCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_UserCategory_IUserCategoryId",
                        column: x => x.IUserCategoryId,
                        principalTable: "UserCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameGameCollection",
                columns: table => new
                {
                    GameCollectionsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameCollection", x => new { x.GameCollectionsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameGameCollection_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGameCollection_GameCollection_GameCollectionsId",
                        column: x => x.GameCollectionsId,
                        principalTable: "GameCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiAccessToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KeyVaultSecretId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiAccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiAccessToken_Subscription_SubscriptionId1",
                        column: x => x.SubscriptionId1,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCategory = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCategoryNavigationId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Subscription_SubscriptionId1",
                        column: x => x.SubscriptionId1,
                        principalTable: "Subscription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_UserCategory_UserCategoryNavigationId",
                        column: x => x.UserCategoryNavigationId,
                        principalTable: "UserCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UserStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "UserStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiAccessToken_SubscriptionId1",
                table: "ApiAccessToken",
                column: "SubscriptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Device_GameId",
                table: "Device",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CategoryId",
                table: "Game",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGameCollection_GamesId",
                table: "GameGameCollection",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSubCollection_GameCollectionId",
                table: "GameSubCollection",
                column: "GameCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserCategoryId",
                table: "Role",
                column: "UserCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IUserCategoryId",
                table: "Subscription",
                column: "IUserCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusId",
                table: "User",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SubscriptionId1",
                table: "User",
                column: "SubscriptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCategoryNavigationId",
                table: "User",
                column: "UserCategoryNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiAccessToken");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "GameGameCollection");

            migrationBuilder.DropTable(
                name: "GameSubCollection");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GameCollection");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserStatus");

            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "UserCategory");
        }
    }
}
