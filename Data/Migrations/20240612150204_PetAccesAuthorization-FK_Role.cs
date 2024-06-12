using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c18_98_m_csharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PetAccesAuthorizationFK_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetAccessAuthorizations_AspNetRoles_RoleId",
                table: "PetAccessAuthorizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "PetAccessAuthorizations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PetAccessAuthorizations_AspNetRoles_RoleId",
                table: "PetAccessAuthorizations",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetAccessAuthorizations_AspNetRoles_RoleId",
                table: "PetAccessAuthorizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "PetAccessAuthorizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PetAccessAuthorizations_AspNetRoles_RoleId",
                table: "PetAccessAuthorizations",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
