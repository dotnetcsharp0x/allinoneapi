using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(17,7)",
                precision: 17,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,15)",
                oldPrecision: 15,
                oldScale: 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(15,15)",
                precision: 15,
                scale: 15,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(17,7)",
                oldPrecision: 17,
                oldScale: 7);
        }
    }
}
