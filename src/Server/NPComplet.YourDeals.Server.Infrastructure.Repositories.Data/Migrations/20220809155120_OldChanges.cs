using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data.Migrations
{
    public partial class OldChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPaid",
            //    schema: "ACCOUNTING",
            //    table: "INVOICES",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "PurchaserId",
            //    schema: "ACCOUNTING",
            //    table: "INVOICES",
            //    type: "uniqueidentifier",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "ACCOUNTING",
                table: "INVOICES");

            migrationBuilder.DropColumn(
                name: "PurchaserId",
                schema: "ACCOUNTING",
                table: "INVOICES");
        }
    }
}
