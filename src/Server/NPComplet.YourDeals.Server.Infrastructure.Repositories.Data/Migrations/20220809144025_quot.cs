using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data.Migrations
{
    public partial class quot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROPOSITIONOFFERS_QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                column: "QuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROPOSITIONOFFERS_QUOTATIONS_QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS",
                column: "QuotationId",
                principalSchema: "ACCOUNTING",
                principalTable: "QUOTATIONS",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROPOSITIONOFFERS_QUOTATIONS_QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS");

            migrationBuilder.DropIndex(
                name: "IX_PROPOSITIONOFFERS_QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS");

            migrationBuilder.DropColumn(
                name: "ProposerId",
                schema: "ACCOUNTING",
                table: "QUOTATIONS");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                schema: "DEAL",
                table: "PROPOSITIONOFFERS");
        }
    }
}
