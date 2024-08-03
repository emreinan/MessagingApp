using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2231ea69-90a3-42fb-8d99-5fcdfeb133e9"));

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("f324baf1-0204-4a24-a859-5279a6a82262"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 215, 131, 79, 242, 26, 130, 241, 37, 20, 150, 254, 238, 17, 112, 96, 245, 246, 2, 246, 226, 98, 11, 125, 76, 56, 0, 165, 217, 157, 33, 51, 49, 127, 109, 47, 64, 156, 50, 238, 41, 40, 204, 145, 52, 89, 14, 65, 248, 33, 16, 182, 120, 113, 177, 193, 125, 187, 233, 121, 27, 120, 156, 218, 165 }, new byte[] { 44, 171, 155, 80, 144, 81, 170, 194, 5, 92, 200, 190, 243, 185, 120, 115, 39, 221, 79, 92, 182, 164, 14, 119, 12, 250, 51, 252, 175, 138, 18, 74, 94, 193, 198, 66, 56, 111, 34, 203, 15, 118, 136, 57, 99, 36, 171, 16, 42, 51, 222, 180, 214, 248, 117, 76, 47, 120, 8, 188, 172, 89, 42, 74, 115, 66, 98, 188, 142, 43, 196, 191, 33, 163, 150, 204, 53, 225, 95, 62, 134, 154, 190, 204, 161, 113, 136, 148, 37, 184, 175, 31, 208, 213, 118, 206, 148, 106, 20, 161, 159, 78, 215, 252, 189, 82, 66, 192, 239, 139, 177, 66, 43, 26, 231, 142, 134, 125, 194, 64, 122, 190, 52, 187, 79, 29, 178, 67 }, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f324baf1-0204-4a24-a859-5279a6a82262"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("2231ea69-90a3-42fb-8d99-5fcdfeb133e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 183, 136, 177, 4, 15, 112, 14, 43, 140, 231, 134, 132, 243, 7, 216, 164, 241, 152, 20, 26, 230, 140, 208, 158, 220, 72, 103, 105, 83, 96, 180, 124, 251, 118, 216, 216, 109, 124, 227, 40, 1, 219, 85, 11, 113, 227, 47, 229, 234, 162, 70, 11, 142, 252, 53, 122, 66, 76, 70, 43, 208, 152, 228, 10 }, new byte[] { 27, 128, 231, 202, 125, 198, 133, 71, 159, 91, 102, 230, 147, 126, 179, 243, 157, 163, 84, 151, 63, 200, 80, 246, 254, 138, 27, 35, 235, 160, 134, 219, 16, 44, 170, 31, 252, 213, 206, 40, 69, 77, 86, 150, 12, 3, 56, 125, 190, 36, 196, 157, 10, 103, 206, 43, 2, 30, 154, 246, 121, 63, 135, 192, 190, 233, 123, 20, 102, 148, 136, 136, 42, 42, 234, 1, 75, 33, 144, 84, 13, 11, 95, 146, 113, 17, 36, 49, 251, 193, 162, 242, 30, 220, 83, 29, 17, 250, 132, 150, 146, 159, 22, 109, 75, 66, 179, 40, 54, 24, 7, 101, 180, 97, 9, 238, 108, 73, 177, 25, 44, 80, 27, 164, 38, 235, 35, 45 }, null, null, null });
        }
    }
}
