using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.API.Interfaces;
using MoreThanFollowUp.API.Services;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Resources;
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
                if (listProjectStatus.IsNullOrEmpty())
                {
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Não iniciado" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Em andamento" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Concluído" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Em revisão" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Em aprovação" });
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



                if (!listProjects.IsNullOrEmpty() || listProjects.Count < 5)
                {
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-1", Responsible = "Nicolas Jeronimo", Category = "Refatoração", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-2", Responsible = "Nicolas Jeronimo", Category = "Inovação", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-3", Responsible = "Nicolas Jeronimo", Category = "Manutenção", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-4", Responsible = "Nicolas Jeronimo", Category = "UX-UI", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-5", Responsible = "Nicolas Jeronimo", Category = "Segurança", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-6", Responsible = "Thiago Henrique", Category = "Integração", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-7", Responsible = "Thiago Henrique", Category = "Refatoração", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-8", Responsible = "Thiago Henrique", Category = "Inovação", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-9", Responsible = "Thiago Henrique", Category = "Manutenção", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-10", Responsible = "Thiago Henrique", Category = "UX-UI", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-11", Responsible = "Richard França", Category = "Segurança", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-12", Responsible = "Richard França", Category = "Integração", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-13", Responsible = "Richard França", Category = "Refatoração", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-14", Responsible = "Richard França", Category = "Inovação", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });                                                                                                                                                                                                                            
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-15", Responsible = "Richard França", Category = "Manutenção", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-16", Responsible = "Richard França", Category = "UX-UI", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-17", Responsible = "Guilherme França", Category = "Segurança", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-18", Responsible = "Guilherme França", Category = "Integração", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-19", Responsible = "Guilherme França", Category = "Refatoração", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null  });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-20", Responsible = "Guilherme França", Category = "Inovação", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-21", Responsible = "Guilherme França", Category = "Manutenção", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-22", Responsible = "Guilherme França", Category = "UX-UI", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-23", Responsible = "Nicolas Jeronim", Category = "Segurança", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-24", Responsible = "Thiago Henrique", Category = "Integração", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-25", Responsible = "Richard França", Category = "Refatoração", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-26", Responsible = "Guilherme França", Category = "Inovação", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-27", Responsible = "Nicolas Jeronimo", Category = "Manutenção", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-28", Responsible = "Thiago Henrique",Category = "UX-UI", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-29", Responsible = "Richard França", Category = "Segurança", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb!.Projects.Add(new Project { Title = "MTFU-30", Responsible = "Guilherme França", Category = "Integração", Status = "Em aprovação", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null });
                    aplicationServiceDb.SaveChanges();
                }
            }
            Console.WriteLine("Done...!");
        }
    }
}
