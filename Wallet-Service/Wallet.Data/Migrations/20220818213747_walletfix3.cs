using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallet.Data.Migrations
{
    public partial class walletfix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "UserBanks");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "UserBanks");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "UserBanks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "UserBanks");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "UserBanks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "UserBanks",
                type: "text",
                nullable: true);
        }
    }
}
