using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PetAccesAuthorizationAddId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PetAccessAuthorizations",
                table: "PetAccessAuthorizations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PetAccessAuthorizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetAccessAuthorizations",
                table: "PetAccessAuthorizations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PetAccessAuthorizations_PetId",
                table: "PetAccessAuthorizations",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PetAccessAuthorizations",
                table: "PetAccessAuthorizations");

            migrationBuilder.DropIndex(
                name: "IX_PetAccessAuthorizations_PetId",
                table: "PetAccessAuthorizations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PetAccessAuthorizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetAccessAuthorizations",
                table: "PetAccessAuthorizations",
                column: "PetId");
        }
    }
}
