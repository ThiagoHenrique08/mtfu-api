using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoreThanFollowUp.API.Extensions;
using MoreThanFollowUp.API.Interfaces;
using MoreThanFollowUp.API.Services;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects.Phases;
using MoreThanFollowUp.Infrastructure.Repository.Projects;
using MoreThanFollowUp.Infrastructure.Repository.Projects.Phases;
using System.Text;

namespace MoreThanFollowUp.API.Extensions
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MorThanFollowUp", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                    {
                                        {
                                            new OpenApiSecurityScheme
                                            {
                                                Reference = new OpenApiReference
                                                {
                                                    Type = ReferenceType.SecurityScheme,
                                                    Id = "Bearer"
                                                }
                                            },
                                            new string[] {}
                                        }
                                    });
            });
            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                        policy =>
                        {
                            /*services.AddCors: Adiciona o serviço CORS e define uma política chamada "AllowAll".
                                policy.AllowAnyOrigin(): Permite qualquer origem.
                                policy.AllowAnyHeader(): Permite qualquer cabeçalho.
                                policy.AllowAnyMethod(): Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.).
                                app.UseCors("AllowAll"): Aplica a política de CORS definida com o nome "AllowAll".*/

                            policy.WithOrigins("https://localhost:7023", "https://localhost:7156", "http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        });
            });

            return services;
        }
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProject_UserRepository, Project_UserRepository>();
            services.AddScoped<IPlanejamentoRepository, PlanejamentoRepository>();

            return services;
        }

        public static IServiceCollection AddTokenService(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection AddContextService(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                            options.UseLazyLoadingProxies();

                        }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, WebApplicationBuilder builder)
        {

            var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secret Key"); //Lança uma exceção se ele não conseguir obter a chave secreta do arquivo de configuração

            services.AddAuthentication(options =>
            {
                //Define que por padrão o sistema irá usar um esquema de Token JWT
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //Lança o Desafio: se alguem tentar acessar um recurso protegido sem passar o token ele irá solicitar as credenciar do usuário
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //Configuração do Token JWT
                options.SaveToken = true; //Indica se o token deve ser salvo
                options.RequireHttpsMetadata = false; //Indica se é preciso HTTPS para transmitir o Token - Em produção colocar true
                options.TokenValidationParameters = new TokenValidationParameters() // configuração dos parametros de autenticação do Token
                {
                    ValidateIssuer = true,//define as configuração de validação do Issuer - Emissor
                    ValidateAudience = true,//define as configuração de validação do Audience - Cliente
                    ValidateLifetime = true, //define a configuração de validação do Tempo de Vida do Token
                    ValidateIssuerSigningKey = true, // validar a chave de assinatura do emissor
                    ClockSkew = TimeSpan.Zero, // Ajustar o tempo para tratar delay entre o servidor de autenticação e servidor de aplicação
                    ValidAudience = builder.Configuration["JWT:ValidAudience"], //atribuir valor na variavel pegando o valor do arquivo de configuração
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],  //atribuir valor na variavel pegando o valor do arquivo de configuração
                    IssuerSigningKey = new SymmetricSecurityKey(
                                        Encoding.UTF8.GetBytes(secretKey)) // Gera uma chave simetrica com base da secretKey para assinar o token 
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                //RequireRole - Exige que o usuário tenha uma determinada Role/Perfil para acessar um recurso protegido
                options.AddPolicy("ScrumMasterOnly", policy => policy.RequireRole("ScrumMaster"));

                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

                //RequireAssertion - Permite definir uma expressão lambda e com uma condição customizada para autorização
                options.AddPolicy("AdminOnly", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "thiagsilva") ||
                                                                        context.User.IsInRole("Admin")));

                options.AddPolicy("AdminOnlyAndScrumMasterOnly", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("ScrumMaster") || context.User.IsInRole("Admin")));

                options.AddPolicy("AdminOnlyAndScrumMasterOnlyAndUserOnly", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("ScrumMaster") || context.User.IsInRole("Admin") || context.User.IsInRole("User")));


                //RequireClaim - Exige que o usuário tenha uma Claim específica para acessar um recurso protegido
                //options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin").RequireClaim("id", "thiagsilva"));

            });

            return services;
        }

        //public static IServiceCollection AddSendGridService(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddSendGrid(options =>
        //    {
        //        options.ApiKey = configuration["SendGridApiKey"];
        //    });

        //    services.AddTransient<IEmailSender, EmailSender>();

        //    return services; // Retorna a IServiceCollection para encadear outras chamadas.
        //}
    }
}

