using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeStack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFolderIdToSynthesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FolderId",
                table: "Syntheses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Syntheses_FolderId",
                table: "Syntheses",
                column: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Syntheses_Folders_FolderId",
                table: "Syntheses",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syntheses_Folders_FolderId",
                table: "Syntheses");

            migrationBuilder.DropIndex(
                name: "IX_Syntheses_FolderId",
                table: "Syntheses");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Syntheses");
        }
    }
}
