using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterandorelacionamentoSprinteUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "SprintScore",
                table: "Sprints",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SprintUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintId = table.Column<int>(type: "INT", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SprintUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintUsers_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SprintUsers_SprintId",
                table: "SprintUsers",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintUsers_UserId",
                table: "SprintUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SprintUsers");

            migrationBuilder.DropColumn(
                name: "SprintScore",
                table: "Sprints");

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
    }
}
