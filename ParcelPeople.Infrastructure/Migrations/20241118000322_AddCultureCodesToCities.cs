using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcelPeople.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCultureCodesToCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CostDisplayed",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CultureCode",
                table: "Cities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CultureCode",
                value: "en-GB");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CultureCode",
                value: "en-IE");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CultureCode",
                value: "en-GB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostDisplayed",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CultureCode",
                table: "Cities");
        }
    }
}
