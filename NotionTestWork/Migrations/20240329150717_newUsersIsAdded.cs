using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotionTestWork.Migrations
{
    /// <inheritdoc />
    public partial class newUsersIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[,]
                {
                    { new Guid("4406b88c-336e-4742-84f0-0e3877accfaa"), "student@gmail.com", "Student" },
                    { new Guid("875387bc-90b7-4766-bfbf-3f8611f5760b"), "prof@gmail.com", "Professor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("4406b88c-336e-4742-84f0-0e3877accfaa"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("875387bc-90b7-4766-bfbf-3f8611f5760b"));
        }
    }
}
