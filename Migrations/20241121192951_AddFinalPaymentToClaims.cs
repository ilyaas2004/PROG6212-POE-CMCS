using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_POE_CMCS.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalPaymentToClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalPayment",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPayment",
                table: "Claims");
        }
    }
}
