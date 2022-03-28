using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagment.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 5, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { new Guid("7646143d-7781-4566-bf49-5aba3751bfa4"), "R--", "only read permission" },
                    { new Guid("5248737e-7105-4b59-be5d-47c3bc72a767"), "-W-", "only write permission" },
                    { new Guid("ac0d40af-f0dd-44bd-ad5d-83ee2bcfa051"), "--X", "only execute permission" },
                    { new Guid("35d77900-e205-4118-9e0a-5b4da53f741e"), "RWX", "all permissions granted" },
                    { new Guid("aef3c363-dbcb-43b9-99ff-197f2dbf9e12"), "---", "no permissions" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Status", "Username" },
                values: new object[,]
                {
                    { new Guid("76b3712d-dcad-4755-b7be-c960ce44c395"), "John@gmail.com", "John", "Wayne", "john123", 2, "john123" },
                    { new Guid("437201cc-8f91-41aa-9b15-99b97de0c229"), "Ana@gmail.com", "Ana", "Smith", "anaSmith1", 0, "ana4" },
                    { new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"), "Sasa@gmail.com", "Joe", "Doe", "Joe23", 1, "Joe45" }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "UserId", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("76b3712d-dcad-4755-b7be-c960ce44c395"), new Guid("aef3c363-dbcb-43b9-99ff-197f2dbf9e12") },
                    { new Guid("437201cc-8f91-41aa-9b15-99b97de0c229"), new Guid("35d77900-e205-4118-9e0a-5b4da53f741e") },
                    { new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"), new Guid("5248737e-7105-4b59-be5d-47c3bc72a767") },
                    { new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"), new Guid("ac0d40af-f0dd-44bd-ad5d-83ee2bcfa051") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
