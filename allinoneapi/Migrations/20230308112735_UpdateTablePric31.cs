using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(19,9)",
                precision: 19,
                scale: 9,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,10)",
                oldPrecision: 17,
                oldScale: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,10)",
                precision: 17,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,9)",
                oldPrecision: 19,
                oldScale: 9);
        }
    }
}
