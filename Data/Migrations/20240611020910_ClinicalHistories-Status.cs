using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClinicalHistoriesStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ClinicalHistoryEntries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ClinicalHistoryEntries");
        }
    }
}
