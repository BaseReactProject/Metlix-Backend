using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailAuthenticators",
                columns: new[] { "Id", "ActivationKey", "CreatedDate", "DeletedDate", "IsVerified", "UpdatedDate", "UserId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, 1 });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Accounts.Admin");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Accounts.Read");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Accounts.Write");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Accounts.Add");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Accounts.Update");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Accounts.Delete");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 78, 235, 115, 7, 140, 99, 109, 130, 186, 17, 58, 110, 130, 210, 38, 129, 3, 111, 251, 19, 2, 183, 116, 173, 67, 231, 70, 186, 183, 121, 84, 166, 224, 218, 202, 227, 123, 96, 101, 106, 62, 145, 83, 178, 124, 19, 254, 154, 98, 244, 105, 22, 165, 149, 190, 176, 3, 103, 174, 177, 26, 129, 116 }, new byte[] { 101, 111, 67, 216, 222, 210, 64, 209, 253, 33, 235, 184, 79, 214, 91, 48, 155, 215, 67, 187, 156, 31, 17, 157, 45, 232, 119, 242, 142, 239, 22, 209, 52, 17, 126, 169, 195, 32, 204, 88, 218, 5, 97, 155, 249, 10, 254, 19, 195, 151, 51, 81, 43, 235, 88, 97, 137, 15, 254, 223, 30, 86, 179, 78, 21, 202, 203, 104, 193, 63, 175, 2, 108, 80, 220, 39, 160, 152, 188, 107, 105, 230, 5, 216, 252, 131, 43, 122, 39, 51, 132, 235, 214, 87, 111, 219, 66, 21, 29, 165, 76, 142, 211, 213, 190, 176, 76, 196, 175, 160, 202, 235, 108, 63, 107, 124, 250, 57, 172, 234, 89, 210, 76, 212, 241, 17, 217, 241 } });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DeleteData(
                table: "EmailAuthenticators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Brands.Admin");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Brands.Read");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Brands.Write");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Brands.Add");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Brands.Update");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Brands.Delete");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Admin", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Read", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Write", null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Add", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Update", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FakeEntities.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 252, 91, 82, 242, 16, 122, 220, 11, 138, 253, 253, 185, 180, 36, 94, 111, 215, 118, 193, 194, 56, 143, 9, 234, 174, 121, 7, 253, 230, 29, 246, 142, 241, 251, 35, 236, 206, 72, 238, 175, 82, 57, 232, 145, 77, 177, 54, 74, 223, 183, 16, 89, 176, 152, 67, 187, 24, 221, 53, 72, 30, 6, 130, 24 }, new byte[] { 88, 116, 51, 106, 103, 156, 134, 14, 12, 243, 148, 19, 35, 220, 77, 155, 250, 182, 54, 68, 121, 149, 82, 73, 106, 98, 52, 54, 126, 74, 120, 8, 7, 135, 190, 232, 173, 117, 72, 149, 250, 79, 171, 1, 137, 119, 135, 64, 193, 102, 66, 132, 99, 209, 111, 138, 201, 50, 93, 220, 106, 127, 82, 144, 219, 48, 150, 3, 118, 103, 120, 29, 122, 100, 62, 53, 96, 207, 127, 253, 143, 87, 82, 9, 217, 150, 173, 53, 188, 88, 242, 197, 52, 10, 60, 33, 41, 96, 150, 170, 92, 139, 154, 232, 9, 141, 242, 124, 182, 170, 54, 7, 251, 131, 128, 250, 58, 120, 196, 83, 95, 244, 202, 247, 248, 90, 33, 145 } });
        }
    }
}
