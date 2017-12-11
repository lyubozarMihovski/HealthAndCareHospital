using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HealthAndCareHospital.Data.Migrations
{
    public partial class RemoveReceiptMedicineRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Receipts_ReceiptId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_ReceiptId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Medicines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_ReceiptId",
                table: "Medicines",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Receipts_ReceiptId",
                table: "Medicines",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
