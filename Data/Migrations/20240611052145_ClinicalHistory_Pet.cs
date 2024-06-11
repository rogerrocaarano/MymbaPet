using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClinicalHistory_Pet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClinicalHistoryId",
                table: "Pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ClinicalHistoryId",
                table: "Pets",
                column: "ClinicalHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_ClinicalHistories_ClinicalHistoryId",
                table: "Pets",
                column: "ClinicalHistoryId",
                principalTable: "ClinicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_ClinicalHistories_ClinicalHistoryId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_ClinicalHistoryId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ClinicalHistoryId",
                table: "Pets");
        }
    }
}
