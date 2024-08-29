using MoreThanFollowUp.API.Extensions;
using System.Net.Security;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//builder.Services.AddSendGridService(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityService();
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
                return sslPolicyErrors == SslPolicyErrors.None || certificate.Issuer == certificate.Subject;
            }
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ApplyMigrations();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("MyPolicy");

app.Run();