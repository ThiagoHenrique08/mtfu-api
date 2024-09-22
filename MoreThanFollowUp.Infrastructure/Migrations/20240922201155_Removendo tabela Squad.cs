using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovendotabelaSquad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Squads_SquadId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Squads");

            migrationBuilder.RenameColumn(
                name: "SquadId",
                table: "Projects",
                newName: "EnterpriseId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_SquadId",
                table: "Projects",
                newName: "IX_Projects_EnterpriseId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectUsers",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Enterprises_EnterpriseId",
                table: "Projects",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "EnterpriseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Enterprises_EnterpriseId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "EnterpriseId",
                table: "Projects",
                newName: "SquadId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_EnterpriseId",
                table: "Projects",
                newName: "IX_Projects_SquadId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectUsers",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Squads",
                columns: table => new
                {
                    SquadId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterpriseId1 = table.Column<int>(type: "INT", nullable: false),
                    EnterpriseId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squads", x => x.SquadId);
                    table.UniqueConstraint("AK_Squads_EnterpriseId", x => x.EnterpriseId);
                    table.ForeignKey(
                        name: "FK_Squads_Enterprises_EnterpriseId1",
                        column: x => x.EnterpriseId1,
                        principalTable: "Enterprises",
                        principalColumn: "EnterpriseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Squads_EnterpriseId1",
                table: "Squads",
                column: "EnterpriseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Squads_SquadId",
                table: "Projects",
                column: "SquadId",
                principalTable: "Squads",
                principalColumn: "EnterpriseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
