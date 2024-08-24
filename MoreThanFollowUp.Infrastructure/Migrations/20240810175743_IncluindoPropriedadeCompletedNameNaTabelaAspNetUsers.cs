using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoPropriedadeCompletedNameNaTabelaAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedName",
                table: "AspNetUsers");
        }
    }
}
