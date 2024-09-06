using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoconfiguracaodatabelaProject_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Plannings",
                columns: table => new
                {
                    PlanningPhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkWebsite = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    ProjectId = table.Column<int>(type: "INT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plannings", x => x.PlanningPhaseId);
                    table.ForeignKey(
                        name: "FK_Plannings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequirementsAnalysis",
                columns: table => new
                {
                    RequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequirementsAnalysis", x => x.RequirementsAnalysPhaseId);
                    table.ForeignKey(
                        name: "FK_RequirementsAnalysis_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintPlanningPhase",
                columns: table => new
                {
                    SprintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningPhaseId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintPlanningPhase", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_SprintPlanningPhase_Plannings_PlanningPhaseId",
                        column: x => x.PlanningPhaseId,
                        principalTable: "Plannings",
                        principalColumn: "PlanningPhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalRequirements",
                columns: table => new
                {
                    FunctionalRequirementsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionOrAction = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Behavior = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    RequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: false),
                    RequirementsAnalysisPhaseRequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalRequirements", x => x.FunctionalRequirementsId);
                    table.ForeignKey(
                        name: "FK_FunctionalRequirements_RequirementsAnalysis_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                        column: x => x.RequirementsAnalysisPhaseRequirementsAnalysPhaseId,
                        principalTable: "RequirementsAnalysis",
                        principalColumn: "RequirementsAnalysPhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotFunctionalRequirements",
                columns: table => new
                {
                    NotFunctionalRequirementsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    RequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: false),
                    RequirementsAnalysisPhaseRequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotFunctionalRequirements", x => x.NotFunctionalRequirementsId);
                    table.ForeignKey(
                        name: "FK_NotFunctionalRequirements_RequirementsAnalysis_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                        column: x => x.RequirementsAnalysisPhaseRequirementsAnalysPhaseId,
                        principalTable: "RequirementsAnalysis",
                        principalColumn: "RequirementsAnalysPhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintRequirementsAnalysisPhase",
                columns: table => new
                {
                    SprintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: false),
                    RequirementsAnalysisPhaseRequirementsAnalysPhaseId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintRequirementsAnalysisPhase", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_SprintRequirementsAnalysisPhase_RequirementsAnalysis_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                        column: x => x.RequirementsAnalysisPhaseRequirementsAnalysPhaseId,
                        principalTable: "RequirementsAnalysis",
                        principalColumn: "RequirementsAnalysPhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalRequirements_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                table: "FunctionalRequirements",
                column: "RequirementsAnalysisPhaseRequirementsAnalysPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_NotFunctionalRequirements_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                table: "NotFunctionalRequirements",
                column: "RequirementsAnalysisPhaseRequirementsAnalysPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_ProjectId",
                table: "Plannings",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementsAnalysis_ProjectId",
                table: "RequirementsAnalysis",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SprintPlanningPhase_PlanningPhaseId",
                table: "SprintPlanningPhase",
                column: "PlanningPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintRequirementsAnalysisPhase_RequirementsAnalysisPhaseRequirementsAnalysPhaseId",
                table: "SprintRequirementsAnalysisPhase",
                column: "RequirementsAnalysisPhaseRequirementsAnalysPhaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionalRequirements");

            migrationBuilder.DropTable(
                name: "NotFunctionalRequirements");

            migrationBuilder.DropTable(
                name: "SprintPlanningPhase");

            migrationBuilder.DropTable(
                name: "SprintRequirementsAnalysisPhase");

            migrationBuilder.DropTable(
                name: "Plannings");

            migrationBuilder.DropTable(
                name: "RequirementsAnalysis");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectUsers",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
