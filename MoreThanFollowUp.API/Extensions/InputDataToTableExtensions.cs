﻿using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;

namespace MoreThanFollowUp.API.Extensions
{
    public static class InputDataToTableExtensions
    {

        public static void InputResourceExtension(this WebApplication app)
        {
            Console.WriteLine("Input data in tables...");
            using (var scope = app.Services.CreateScope())
            {
                var aplicationServiceDb = scope.ServiceProvider
                                 .GetService<ApplicationDbContext>();
                var listProjectStatus = aplicationServiceDb!.ProjectStatus.ToList();
                var listCategories = aplicationServiceDb!.ProjectStatus.ToList();
                var listProjects = aplicationServiceDb!.Projects.ToList();
                var listResponsible = aplicationServiceDb?.Responsible.ToList();
                var listTenant = aplicationServiceDb?.Tenants.ToList();
                var listEnterprise = aplicationServiceDb?.Enteprises.ToList();
                var listSubscription = aplicationServiceDb?.Subscriptions.ToList();
                var listInvoice = aplicationServiceDb?.Invoices.ToList();
                var listPlanning = aplicationServiceDb?.Plannings.ToList();
                var listUsers = aplicationServiceDb?.Users.ToList();
                var listRoles = aplicationServiceDb?.Roles.ToList();

                var listSprint = aplicationServiceDb?.Sprints.ToList();
                var listRequirementAnalysis = aplicationServiceDb?.RequirementAnalysis.ToList();

                if (listProjectStatus.IsNullOrEmpty())
                {
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Pendente" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Em andamento" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Concluído" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Cancelado" });

                    aplicationServiceDb.SaveChanges();
                }
                if (listCategories.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "Refatoração" });
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "Inovação" });
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "Manutenção" });
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "UX-UI" });
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "Segurança" });
                    aplicationServiceDb!.Categories.Add(new ProjectCategory { Name = "Integração" });
                    aplicationServiceDb.SaveChanges();
                }

                if (listResponsible.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Thiago Henrique" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Nicolas Jeronimo" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Guilherme França" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Richard França" });

                    aplicationServiceDb.SaveChanges();
                }
                if (listTenant.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Tenants.Add(new Tenant { TenantName = "SWIF", TenantCustomDomain = "swif.com.br", TenantStatus = "Ativo", Responsible = "Thiago Henrique", Email = "thiago@gmail.com", PhoneNumber = "119174642292", CreatedAt = DateTime.Now, UpdateAt = null });

                    aplicationServiceDb.SaveChanges();
                }

                if (listSubscription.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Subscriptions.Add(new Subscription { TenantId = 1, Plan = "Premium", Status = "Ativo", TotalLicense = 50, TotalUsed = 10, TotalAvailable = 40, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(365) });

                    aplicationServiceDb.SaveChanges();
                }

                if (listInvoice.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Invoices.Add(new Invoice { Amount = 15000, Status = "Ativo", SubscriptionId = 1, CreateAt = DateTime.Now, DueDate = DateTime.Now.AddDays(365) });

                    aplicationServiceDb.SaveChanges();
                }

                if (listEnterprise.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Enteprises.Add(new Enterprise { CorporateReason = "SWIF Technology LTDA", CNPJ = "09.001.001/0001-10", Segment = "Tecnologia", TenantId = 1 });

                    aplicationServiceDb.SaveChanges();
                }


                if (listProjects.IsNullOrEmpty() || listProjects.Count < 5)
                {
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-1", Responsible = "Nicolas Jeronimo", Category = "Refatoração", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-2", Responsible = "Nicolas Jeronimo", Category = "Inovação", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-3", Responsible = "Nicolas Jeronimo", Category = "Manutenção", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-4", Responsible = "Nicolas Jeronimo", Category = "UX-UI", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-5", Responsible = "Nicolas Jeronimo", Category = "Segurança", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-6", Responsible = "Thiago Henrique", Category = "Integração", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-7", Responsible = "Thiago Henrique", Category = "Refatoração", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-8", Responsible = "Thiago Henrique", Category = "Inovação", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-9", Responsible = "Thiago Henrique", Category = "Manutenção", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-10", Responsible = "Thiago Henrique", Category = "UX-UI", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-11", Responsible = "Richard França", Category = "Segurança", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-12", Responsible = "Richard França", Category = "Integração", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-13", Responsible = "Richard França", Category = "Refatoração", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-14", Responsible = "Richard França", Category = "Inovação", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-15", Responsible = "Richard França", Category = "Manutenção", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-16", Responsible = "Richard França", Category = "UX-UI", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-17", Responsible = "Guilherme França", Category = "Segurança", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-18", Responsible = "Guilherme França", Category = "Integração", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-19", Responsible = "Guilherme França", Category = "Refatoração", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-20", Responsible = "Guilherme França", Category = "Inovação", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-21", Responsible = "Guilherme França", Category = "Manutenção", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-22", Responsible = "Guilherme França", Category = "UX-UI", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-23", Responsible = "Nicolas Jeronim", Category = "Segurança", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-24", Responsible = "Thiago Henrique", Category = "Integração", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-25", Responsible = "Richard França", Category = "Refatoração", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-26", Responsible = "Guilherme França", Category = "Inovação", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-27", Responsible = "Nicolas Jeronimo", Category = "Manutenção", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-28", Responsible = "Thiago Henrique", Category = "UX-UI", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-29", Responsible = "Richard França", Category = "Segurança", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-30", Responsible = "Guilherme França", Category = "Integração", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = 1 });
                    aplicationServiceDb.SaveChanges();
                }

                if (listPlanning.IsNullOrEmpty())
                {
                    var listProjects2 = aplicationServiceDb!.Projects.ToList();
                    var totalProjects = listProjects2.Count;

                    for (int i = 1; i <= totalProjects; i++)
                    {
                        aplicationServiceDb!.Plannings.Add(new Planning

                        {
                            StartDate = DateTime.Now,
                            EndDate = null,
                            DocumentationLink = "link.com.br",
                            PlanningDescription = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"",
                            ProjectId = i,
                            
                        });
                        aplicationServiceDb.SaveChanges();

                    }


                    var plannings = aplicationServiceDb!.Plannings.ToList();


                    foreach (var p in plannings)
                    {
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 1", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, PlanningId = p.PlanningId, Planning = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 2", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, PlanningId = p.PlanningId, Planning = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 3", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, PlanningId = p.PlanningId, Planning = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 4", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Em andamento", SprintScore = 60, PlanningId = p.PlanningId, Planning = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 5", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Pendente", SprintScore = 60, PlanningId = p.PlanningId, Planning = p });
                        aplicationServiceDb.SaveChanges();
                    }

                }

                if (listRequirementAnalysis.IsNullOrEmpty())
                {
                    var listProjects2 = aplicationServiceDb!.Projects.ToList();
                    var totalProjects = listProjects2.Count;

                    for (int i = 1; i <= totalProjects; i++)
                    {
                        aplicationServiceDb!.RequirementAnalysis.Add(new RequirementAnalysis

                        {
                            StartDate = DateTime.Now,
                            EndDate = null,
                            ProjectId = i,
                        });
                        aplicationServiceDb.SaveChanges();

                    }

                    var RequirementAnalysis = aplicationServiceDb!.RequirementAnalysis.ToList();


                    foreach (var p in RequirementAnalysis)
                    {
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 1", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, RequirementAnalysisId = p.RequirementAnalysisId, RequirementAnalysis = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 2", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, RequirementAnalysisId = p.RequirementAnalysisId, RequirementAnalysis = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 3", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Concluído", SprintScore = 60, RequirementAnalysisId = p.RequirementAnalysisId, RequirementAnalysis = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 4", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Em andamento", SprintScore = 60, RequirementAnalysisId = p.RequirementAnalysisId, RequirementAnalysis = p });
                        aplicationServiceDb.Sprints.Add(new Sprint { Title = "MTFU Sprint 5", Description = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"", StartDate = DateTime.Now, EndDate = null, Status = "Pendente", SprintScore = 60, RequirementAnalysisId = p.RequirementAnalysisId, RequirementAnalysis = p });

                        aplicationServiceDb.SaveChanges();
                    }

                }


            }
            Console.WriteLine("Done...!");
        }
    }
}
