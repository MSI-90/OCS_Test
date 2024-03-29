using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotionTestWork.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAttrAsUnsubmittedFromApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unsubmitted",
                table: "applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Unsubmitted",
                table: "applications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
