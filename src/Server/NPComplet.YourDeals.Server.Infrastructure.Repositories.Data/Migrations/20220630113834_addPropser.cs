using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data.Migrations
{
    public partial class addPropser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProposerId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProposerId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS");
        }
    }
}
