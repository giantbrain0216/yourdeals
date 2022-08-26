using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data.Migrations
{
    public partial class QuotationAcceptReject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                schema: "ACCOUNTING",
                table: "QUOTATIONS");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                schema: "ACCOUNTING",
                table: "QUOTATIONS");
        }
    }
}
