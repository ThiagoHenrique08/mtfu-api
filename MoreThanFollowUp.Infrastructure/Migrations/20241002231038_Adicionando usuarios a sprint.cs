using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandousuariosasprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SprintId",
                table: "AspNetUsers",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SprintId",
                table: "AspNetUsers",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sprints_SprintId",
                table: "AspNetUsers",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sprints_SprintId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SprintId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SprintId",
                table: "AspNetUsers");
        }
    }
}
