using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotionTestWork.Migrations
{
    /// <inheritdoc />
    public partial class newAttributAsIsSubmitted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("289236d5-fad6-43ba-a65f-6155d2c09f8f"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("b50ac3d1-bce6-4b77-8221-886fa7ae8566"));

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "applications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "applications");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[,]
                {
                    { new Guid("289236d5-fad6-43ba-a65f-6155d2c09f8f"), "student@gmail.com", "Student" },
                    { new Guid("b50ac3d1-bce6-4b77-8221-886fa7ae8566"), "prof@gmail.com", "Professor" }
                });
        }
    }
}
