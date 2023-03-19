using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,12)",
                precision: 17,
                scale: 12,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,8)",
                oldPrecision: 17,
                oldScale: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,8)",
                precision: 17,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,12)",
                oldPrecision: 17,
                oldScale: 12);
        }
    }
}
