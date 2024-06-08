using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Patients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "LastEntryId",
                table: "ClinicalHistories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "ClinicalHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ClinicalHistoryEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicalHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalHistoryEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalHistoryEntry_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistoryEntry_ClinicalHistoryId",
                table: "ClinicalHistoryEntry",
                column: "ClinicalHistoryId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalHistoryEntry");

            migrationBuilder.DropColumn(
                name: "LastEntryId",
                table: "ClinicalHistories");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "ClinicalHistories");

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
    }
}
