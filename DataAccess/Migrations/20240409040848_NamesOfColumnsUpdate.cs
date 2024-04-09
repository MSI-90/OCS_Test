using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NamesOfColumnsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Outline",
                table: "applications",
                newName: "outline");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "applications",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "applications",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Activity",
                table: "applications",
                newName: "activity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "applications",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsSubmitted",
                table: "applications",
                newName: "is_submitted");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "applications",
                newName: "crated_time");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "applications",
                newName: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "outline",
                table: "applications",
                newName: "Outline");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "applications",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "applications",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "activity",
                table: "applications",
                newName: "Activity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "applications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_submitted",
                table: "applications",
                newName: "IsSubmitted");

            migrationBuilder.RenameColumn(
                name: "crated_time",
                table: "applications",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "applications",
                newName: "Author");
        }
    }
}
