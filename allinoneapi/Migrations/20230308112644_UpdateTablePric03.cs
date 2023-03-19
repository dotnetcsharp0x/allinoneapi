using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,10)",
                precision: 17,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,7)",
                oldPrecision: 17,
                oldScale: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,7)",
                precision: 17,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,10)",
                oldPrecision: 17,
                oldScale: 10);
        }
    }
}
