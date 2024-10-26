using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ff72622-d92e-4404-971e-5dabc3999f55"));

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.CreateTable(
                name: "CopyOfUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    PhotoData = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Age = table.Column<byte>(type: "smallint", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopyOfUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Bio", "CreatedDateTime", "FirstName", "Gender", "LastName", "PasswordHash", "PhotoData", "Role", "Username" },
                values: new object[] { new Guid("e8a46103-2d84-41b8-9d30-83286e07c269"), null, null, new DateTime(2024, 10, 26, 10, 34, 19, 860, DateTimeKind.Utc).AddTicks(6180), "Admin", "male", "Admin", "AQAAAAIAAYagAAAAENp364w9cCpKAr3syY2UrDvBbTwN1tu8lmVtNNBwYsNTulNsLGdo2rfw35YJlg9BMQ==", null, "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopyOfUsers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e8a46103-2d84-41b8-9d30-83286e07c269"));

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Bio", "CreatedDateTime", "FirstName", "Gender", "LastName", "PasswordHash", "PhotoData", "Role", "UserName" },
                values: new object[] { new Guid("7ff72622-d92e-4404-971e-5dabc3999f55"), null, null, new DateTime(2024, 10, 1, 13, 49, 36, 917, DateTimeKind.Utc).AddTicks(464), "Admin", "male", "Admin", "AQAAAAIAAYagAAAAEFO7DumY20iF8G+qqYMVTkzC0r/CahgflebFUmCqt5avHwYzDkM9UUtH7qW7E8gXDw==", null, "admin", "admin" });
        }
    }
}
