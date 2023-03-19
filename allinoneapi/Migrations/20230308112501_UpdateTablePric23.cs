using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,11)",
                precision: 17,
                scale: 11,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,12)",
                oldPrecision: 17,
                oldScale: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,12)",
                precision: 17,
                scale: 12,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,11)",
                oldPrecision: 17,
                oldScale: 11);
        }
    }
}
