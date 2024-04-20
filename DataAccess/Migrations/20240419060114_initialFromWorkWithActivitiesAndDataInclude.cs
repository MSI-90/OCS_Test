using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialFromWorkWithActivitiesAndDataInclude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "activity_kynd",
                table: "activities",
                newName: "activity_kind");

            migrationBuilder.InsertData(
                table: "activities",
                columns: new[] { "id", "description", "activity_kind" },
                values: new object[,]
                {
                    { 1, "Доклад, 35 - 45 минут", (byte)0 },
                    { 2, "Мастеркласс, 1-2 часа", (byte)1 },
                    { 3, "Дискуссия/круглый стол, 40-50 минут", (byte)2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "activity_kind",
                table: "activities",
                newName: "activity_kynd");
        }
    }
}
