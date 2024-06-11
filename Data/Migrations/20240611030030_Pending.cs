using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PetId",
                table: "ClinicalHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistories_PetId",
                table: "ClinicalHistories",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalHistories_Pets_PetId",
                table: "ClinicalHistories",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalHistories_Pets_PetId",
                table: "ClinicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalHistories_PetId",
                table: "ClinicalHistories");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "ClinicalHistories");
        }
    }
}
