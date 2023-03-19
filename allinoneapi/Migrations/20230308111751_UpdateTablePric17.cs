using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace allinoneapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePric17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(18,15)",
                precision: 18,
                scale: 15,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,18)",
                oldPrecision: 18,
                oldScale: 18);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Crypto_Price",
                type: "decimal(18,18)",
                precision: 18,
                scale: 18,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,15)",
                oldPrecision: 18,
                oldScale: 15);
        }
    }
}
