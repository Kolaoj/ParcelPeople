using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcelPeople.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerEmailNumberUniqueColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email_ContactNumber",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactNumber",
                table: "Customers",
                column: "ContactNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_ContactNumber",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email_ContactNumber",
                table: "Customers",
                columns: new[] { "Email", "ContactNumber" },
                unique: true);
        }
    }
}
