﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    EnterpriseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CorporateReason = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    CNPJ = table.Column<string>(type: "VARCHAR(18)", nullable: true),
                    Segment = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.EnterpriseId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    StatusProjectId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatus", x => x.StatusProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Responsible",
                columns: table => new
                {
                    ResponsibleId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsible", x => x.ResponsibleId);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TenantName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    TenantCustomDomain = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    TenantStatus = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Responsible = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Responsible = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Category = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EnterpriseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "EnterpriseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoleEnterprisesTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EnterpriseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    TenantId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoleEnterprisesTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoleEnterprisesTenants_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoleEnterprisesTenants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoleEnterprisesTenants_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "EnterpriseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoleEnterprisesTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TenantId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Plan = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    TotalLicense = table.Column<int>(type: "INT", nullable: true),
                    TotalAvailable = table.Column<int>(type: "INT", nullable: true),
                    TotalUsed = table.Column<int>(type: "INT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plannings",
                columns: table => new
                {
                    PlanningId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    DocumentationLink = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    PlanningDescription = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    StartDate = table.Column<DateTime>(name: "Start Date", type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(name: "End Date", type: "DATETIME", nullable: true),
                    ProjectId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plannings", x => x.PlanningId);
                    table.ForeignKey(
                        name: "FK_Plannings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ProjectId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequirementAnalysis",
                columns: table => new
                {
                    RequirementAnalysisId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    StartDate = table.Column<DateTime>(name: "Start Date", type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(name: "End Date", type: "DATETIME", nullable: true),
                    ProjectId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequirementAnalysis", x => x.RequirementAnalysisId);
                    table.ForeignKey(
                        name: "FK_RequirementAnalysis_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DueDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectOrFunctionalRequirements",
                columns: table => new
                {
                    DirectOrFunctionalRequirementId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FunctionOrAction = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    SystemBehavior = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    RequirementAnalysisId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectOrFunctionalRequirements", x => x.DirectOrFunctionalRequirementId);
                    table.ForeignKey(
                        name: "FK_DirectOrFunctionalRequirements_RequirementAnalysis_RequirementAnalysisId",
                        column: x => x.RequirementAnalysisId,
                        principalTable: "RequirementAnalysis",
                        principalColumn: "RequirementAnalysisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Link = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    StartDate = table.Column<DateTime>(name: "Start Date", type: "DATETIME", nullable: true),
                    EndDate = table.Column<DateTime>(name: "End Date", type: "DATETIME", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    SprintScore = table.Column<int>(type: "INT", nullable: true),
                    PlanningId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    RequirementAnalysisId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_Sprints_Plannings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Plannings",
                        principalColumn: "PlanningId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sprints_RequirementAnalysis_RequirementAnalysisId",
                        column: x => x.RequirementAnalysisId,
                        principalTable: "RequirementAnalysis",
                        principalColumn: "RequirementAnalysisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SprintUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SprintId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
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
                name: "IX_ApplicationUserRoleEnterprisesTenants_EnterpriseId",
                table: "ApplicationUserRoleEnterprisesTenants",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoleEnterprisesTenants_RoleId",
                table: "ApplicationUserRoleEnterprisesTenants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoleEnterprisesTenants_TenantId",
                table: "ApplicationUserRoleEnterprisesTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoleEnterprisesTenants_UserId",
                table: "ApplicationUserRoleEnterprisesTenants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DirectOrFunctionalRequirements_RequirementAnalysisId",
                table: "DirectOrFunctionalRequirements",
                column: "RequirementAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SubscriptionId",
                table: "Invoices",
                column: "SubscriptionId",
                unique: true,
                filter: "[SubscriptionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_ProjectId",
                table: "Plannings",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EnterpriseId",
                table: "Projects",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserId",
                table: "ProjectUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementAnalysis_ProjectId",
                table: "RequirementAnalysis",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_PlanningId",
                table: "Sprints",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_RequirementAnalysisId",
                table: "Sprints",
                column: "RequirementAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintUsers_SprintId",
                table: "SprintUsers",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintUsers_UserId",
                table: "SprintUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRoleEnterprisesTenants");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DirectOrFunctionalRequirements");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ProjectStatus");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Responsible");

            migrationBuilder.DropTable(
                name: "SprintUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Plannings");

            migrationBuilder.DropTable(
                name: "RequirementAnalysis");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Enterprises");
        }
    }
}
