using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Login;
using MoreThanFollowUp.Application.DTO.Project;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Repository.Entities.Projects;

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
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Pendente" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Em andamento" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Concluído" });
                    aplicationServiceDb!.ProjectStatus.Add(new ProjectStatus { Name = "Cancelado" });

                    aplicationServiceDb.SaveChanges();
                    Console.WriteLine("Input ProjecStatus Ok!...");
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
                    Console.WriteLine("Input Categories Ok!...");
                }

                if (listResponsible.IsNullOrEmpty())
                {
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Thiago Henrique" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Nicolas Jeronimo" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Guilherme França" });
                    aplicationServiceDb!.Responsible.Add(new ProjectResponsible { Name = "Richard França" });

                    aplicationServiceDb.SaveChanges();
                    Console.WriteLine("Input Responsible Ok!...");
                }

                // Obtenha o UserManager para manipular usuários do Identity
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var tenant = scope.ServiceProvider.GetRequiredService<ITenantRepository>();
                var enterprise = scope.ServiceProvider.GetRequiredService<IEnterpriseRepository>();
                var enterprise_User = scope.ServiceProvider.GetRequiredService<IEnterprise_UserRepository>();
                var user_role_enterprise = scope.ServiceProvider.GetRequiredService<IApplicationUserRoleEnterpriseRepository>();

                var project = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                var project_user = scope.ServiceProvider.GetRequiredService<IProject_UserRepository>();
                var planning = scope.ServiceProvider.GetRequiredService<IPlanningRepository>();

                // Chame o método assíncrono de forma síncrona com GetAwaiter().GetResult() (caso seu método Configure não seja assíncrono)
                Console.WriteLine("Input Data Users...");
                ImputeDataInTheEnvironment(userManager, roleManager, tenant, enterprise, enterprise_User, user_role_enterprise).GetAwaiter().GetResult();

                Console.WriteLine("Input Data Projects...");
                ImputeDataTheProjects(project, project_user, enterprise, userManager, planning).GetAwaiter().GetResult();

            }
        }

        private static async Task ImputeDataTheProjects(IProjectRepository _project, IProject_UserRepository _project_user, IEnterpriseRepository _enterprise, UserManager<ApplicationUser> _userManager, IPlanningRepository _planning)
        {
            var userList = await _userManager.Users.ToListAsync();

            var EnterpriseList = await _enterprise.ToListAsync();

            if (EnterpriseList.IsNullOrEmpty())
            {
                foreach (var enterprise in EnterpriseList)
                {
                    var projectList = new List<Project>();
                    var project1 = new Project { Title = "MTFU-1", Responsible = "Nicolas Jeronimo", Category = "Refatoração", Status = "Não iniciado", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = enterprise.EnterpriseId };
                    var project2 = new Project { Title = "MTFU-2", Responsible = "Thiago Henrique", Category = "Inovação", Status = "Em andamento", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = enterprise.EnterpriseId };
                    var project3 = new Project { Title = "MTFU-3", Responsible = "Richard França", Category = "Manutenção", Status = "Concluído", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = enterprise.EnterpriseId };
                    var project4 = new Project { Title = "MTFU-4", Responsible = "Guilherme França", Category = "UX-UI", Status = "Em revisão", Description = "Projeto da SWIF Tecnology", EndDate = null, CreateDate = DateTime.Now, Projects_Users = null, EnterpriseId = enterprise.EnterpriseId };

                    projectList.Add(project1);
                    projectList.Add(project2);
                    projectList.Add(project3);
                    projectList.Add(project4);

                    await _project.RegisterList(projectList);
                    Console.WriteLine("");
                    Console.WriteLine($"Projects the Enteprise {enterprise.CorporateReason} Created with successfull!");

                    var listProjectCreated = _project.SearchForAsync(p => p.EnterpriseId == enterprise.EnterpriseId);
                    var project_userList = new List<Project_User>();
                    foreach (var project in listProjectCreated)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"Creating Planning per Project:  {project.Title}...");

                        var newPlanning = new Planning

                        {
                            StartDate = DateTime.Now,
                            EndDate = null,
                            DocumentationLink = "link.com.br",
                            PlanningDescription = "\"[\\n  {\\n    \\\"id\\\": \\\"9f93c501-9747-47d7-9a70-790386add372\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [\\n      {\\n        \\\"type\\\": \\\"text\\\",\\n        \\\"text\\\": \\\"AAAAAAAAAAAAAAAAAAAAAAAAA\\\",\\n        \\\"styles\\\": {}\\n      }\\n    ],\\n    \\\"children\\\": []\\n  },\\n  {\\n    \\\"id\\\": \\\"9ec4df74-a85c-456c-bbb1-7e4373eb11aa\\\",\\n    \\\"type\\\": \\\"paragraph\\\",\\n    \\\"props\\\": {\\n      \\\"textColor\\\": \\\"default\\\",\\n      \\\"backgroundColor\\\": \\\"default\\\",\\n      \\\"textAlignment\\\": \\\"left\\\"\\n    },\\n    \\\"content\\\": [],\\n    \\\"children\\\": []\\n  }\\n]\"",
                            ProjectId = project.ProjectId,
                            Project = project
                        };
                        await _planning.RegisterAsync(newPlanning);

                        Console.WriteLine("");
                        Console.WriteLine($"Planning per Project:  {project.Title}, Created with successfull!");

                        foreach (var user in userList)
                        {
                            project_userList.Add(new Project_User
                            {
                                Project = project,
                                User = user,
                                CreateDate = DateTime.Now
                            });
                            Console.WriteLine("");
                            Console.WriteLine($"Relationship the  {project.Title} and {user.CompletedName} Created with successfull!");
                        }
                        await _project_user.RegisterList(project_userList);
                        project_userList.Clear();
                        Console.WriteLine("");
                        Console.WriteLine($"Relationship the  all users Created with successfull!");


                    }
                    Console.WriteLine("Creating projects next Enterprise...");
                }
            }
        }

        private static async Task ImputeDataInTheEnvironment(UserManager<ApplicationUser> _userManager,
                                                                RoleManager<ApplicationRole> _roleManager,
                                                                       ITenantRepository _tenant,
                                                                            IEnterpriseRepository _enterprise,
                                                                                  IEnterprise_UserRepository _enterprise_user,
                                                                                       IApplicationUserRoleEnterpriseRepository _user_role_enterprise)
        {
            var listUser = new List<RegisterModel>();
            // Verifique se o usuário já existe para evitar duplicação



            var user1 = new RegisterModel
            {
                Username = "thiagsilva",
                Email = "thiago@gmail.com",
                Function = "Backend",
                CompletedName = "Thiago Henrique",
                Password = "Criativos1@",
                EnterpriseName = "B3"

                // Configure outras propriedades necessárias
            };

            var user2 = new RegisterModel
            {
                Username = "nicolas",
                Email = "nicolas@gmail.com",
                Function = "Frontend",
                CompletedName = "Nicolas Jeronimo",
                Password = "Criativos1@",
                EnterpriseName = "Return"
                // Configure outras propriedades necessárias
            };

            var user3 = new RegisterModel
            {
                Username = "richard",
                Email = "richard@gmail.com",
                Function = "Frontend",
                CompletedName = "Richard França",
                Password = "Criativos1@",
                EnterpriseName = "Atos"
                // Configure outras propriedades necessárias
            };

            var user4 = new RegisterModel
            {
                Username = "guilherme",
                Email = "guilherme@gmail.com",
                Function = "Frontend",
                CompletedName = "Guilherme França",
                Password = "Criativos1@",
                EnterpriseName = "Geotab"
                // Configure outras propriedades necessárias
            };
            listUser.Add(user1);
            listUser.Add(user2);
            listUser.Add(user3);
            listUser.Add(user4);




            foreach (var user in listUser)
            {

                var existingUser = await _userManager.FindByNameAsync(user.Username!);

                if (existingUser == null)
                {
                    var applicationUser = new ApplicationUser
                    {
                        Email = user.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = user.Username,
                        Function = user.Function,
                        CompletedName = user.CompletedName,
                    };


                    // Crie o usuário no banco de dados com uma senha padrão
                    var result = await _userManager.CreateAsync(applicationUser, user.Password!);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Falha ao criar o usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    var createdUser = await _userManager.FindByEmailAsync(applicationUser.Email!);
                    Console.WriteLine($"User {createdUser!.CompletedName}, created with successfull...");


                    //CRIA UM NOVO TENANT
                    //============================================================================
                    var newTenant = new Tenant
                    {
                        TenantName = user.EnterpriseName,
                        TenantCustomDomain = null,
                        TenantStatus = "Ativo",
                        Responsible = user.CompletedName,
                        Email = user.Email,
                        PhoneNumber = null,
                        CreatedAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };
                    var tenantCreated = await _tenant.RegisterAsync(newTenant);
                    Console.WriteLine($"Tenant {tenantCreated.TenantName}, created with successfull...");
                    //============================================================================


                    //CRIA UMA NOVA EMPRESA E ASSOCIA AO TENANT CRIADO ACIMA
                    //============================================================================
                    var newEnterprise = new Enterprise
                    {
                        CorporateReason = user.EnterpriseName,
                        CNPJ = null,
                        Segment = null,
                        TenantId = tenantCreated.TenantId,
                        Tenant = tenantCreated
                    };
                    var enterpriseCreated = await _enterprise.RegisterAsync(newEnterprise);
                    Console.WriteLine($"Enterprise {enterpriseCreated.CorporateReason}, created with successfull...");
                    //============================================================================


                    //CRIA O RELACIONAMENTO USUARIO E EMPRESA N:N
                    //============================================================================
                    var enterpriseUser = new Enterprise_User
                    {
                        EnterpriseId = enterpriseCreated.EnterpriseId,
                        Enterprise = enterpriseCreated,
                        User = createdUser,
                    };
                    await _enterprise_user.RegisterAsync(enterpriseUser);
                    Console.WriteLine($"relationship between  {enterpriseUser.Enterprise.CorporateReason} e {enterpriseUser.User!.CompletedName}, created with successfull...");

                    //============================================================================

                    //CRIA A ROLE ADMIN CASO ELA NÃO EXISTA
                    //============================================================================
                    var roleExist = await _roleManager.RoleExistsAsync("ADMIN");

                    if (!roleExist)
                    {
                        ApplicationRole role = new()
                        {
                            Name = "ADMIN",
                            NormalizedName = "ADMIN".ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString(),
                        };
                        await _roleManager.CreateAsync(role);
                        Console.WriteLine($"Role  {role.Name}, created with successfull...");
                    }

                    //ADICIONA A ROLE DO USUÁRIO NA CRIAÇÃO
                    //============================================================================
                    var recoverUser = await _userManager.FindByEmailAsync(createdUser!.Email!);
                    var recoverRole = await _roleManager.FindByNameAsync("ADMIN");
                    var recoverEnterprise = await _enterprise.RecoverBy(p => p.EnterpriseId == enterpriseCreated.EnterpriseId);

                    if (recoverUser != null)
                    {
                        //var result = await _userManager.AddToRoleAsync(user, roleName);
                        var roleToUserObject = new ApplicationUserRoleEnterprise
                        {
                            UserId = recoverUser!.Id,
                            User = recoverUser,
                            RoleId = recoverRole!.Id,
                            Role = recoverRole,
                            EnterpriseId = recoverEnterprise!.EnterpriseId,
                            Enterprise = recoverEnterprise

                        };
                        await _user_role_enterprise.RegisterAsync(roleToUserObject);
                        Console.WriteLine($"relationship between  {roleToUserObject.User.CompletedName} e {roleToUserObject.Enterprise!.CorporateReason} e {roleToUserObject.Role.Name}, created with successfull...");
                        //=========================================================================

                    }

                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}

