using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClinicalEntriesInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalHistoryEntry_ClinicalHistories_ClinicalHistoryId",
                table: "ClinicalHistoryEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicalHistoryEntry",
                table: "ClinicalHistoryEntry");

            migrationBuilder.RenameTable(
                name: "ClinicalHistoryEntry",
                newName: "ClinicalHistoryEntries");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "ClinicalHistoryEntries",
                newName: "LastUpdated");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicalHistoryEntry_ClinicalHistoryId",
                table: "ClinicalHistoryEntries",
                newName: "IX_ClinicalHistoryEntries_ClinicalHistoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ClinicalHistoryEntries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicalHistoryEntries",
                table: "ClinicalHistoryEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalHistoryEntries_ClinicalHistories_ClinicalHistoryId",
                table: "ClinicalHistoryEntries",
                column: "ClinicalHistoryId",
                principalTable: "ClinicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalHistoryEntries_ClinicalHistories_ClinicalHistoryId",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicalHistoryEntries",
                table: "ClinicalHistoryEntries");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ClinicalHistoryEntries");

            migrationBuilder.RenameTable(
                name: "ClinicalHistoryEntries",
                newName: "ClinicalHistoryEntry");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "ClinicalHistoryEntry",
                newName: "DateTime");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicalHistoryEntries_ClinicalHistoryId",
                table: "ClinicalHistoryEntry",
                newName: "IX_ClinicalHistoryEntry_ClinicalHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicalHistoryEntry",
                table: "ClinicalHistoryEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalHistoryEntry_ClinicalHistories_ClinicalHistoryId",
                table: "ClinicalHistoryEntry",
                column: "ClinicalHistoryId",
                principalTable: "ClinicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
