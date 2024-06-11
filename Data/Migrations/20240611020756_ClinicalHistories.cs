using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClinicalHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ClinicalHistoryEntries",
                newName: "Treatment");

            migrationBuilder.AddColumn<string>(
                name: "ConsultReason",
                table: "ClinicalHistoryEntries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "ClinicalHistoryEntries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "ClinicalHistoryEntries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PetWeight",
                table: "ClinicalHistoryEntries",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "ClinicalHistoryEntries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClinicalHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultReason",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "PetWeight",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClinicalHistories");

            migrationBuilder.RenameColumn(
                name: "Treatment",
                table: "ClinicalHistoryEntries",
                newName: "Description");
        }
    }
}
