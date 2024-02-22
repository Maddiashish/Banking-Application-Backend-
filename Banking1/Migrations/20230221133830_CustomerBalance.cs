using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking1.Migrations
{
    /// <inheritdoc />
    public partial class CustomerBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "balance",
                table: "CustomerBalance",
                newName: "AccountBalance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountBalance",
                table: "CustomerBalance",
                newName: "balance");
        }
    }
}
