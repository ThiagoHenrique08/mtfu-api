using MoreThanFollowUp.API.Extensions;
using System.Net.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsService();
builder.Services.AddDomainService();
builder.Services.AddTokenService();
builder.Services.AddContextService();
builder.Services.AddAuthenticationService(builder);
builder.Services.AddAuthorizationService();

builder.Services.AddHttpClient("MyClient")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
            {
                // Exemplo: Aceitando todos os certificados (apenas para desenvolvimento)
                return sslPolicyErrors == SslPolicyErrors.None || certificate!.Issuer == certificate.Subject;
            }
        };
    });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        // Outras configurações podem ser adicionadas aqui
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Para ignorar valores nulos
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ApplyMigrations();
app.InputResourceExtension();       
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("MyPolicy");

app.Run();