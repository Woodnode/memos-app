using Microsoft.EntityFrameworkCore;
using MemosWebInfrastructure.Data;
using MemosWebApplicationCore.Interfaces;
using MemosWebApplicationCore.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://memos-web-votre-nom.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API M�mos",
        Version = "v1",
        Description = "Une API pour g�rer les comptes utilisateurs et les m�mos.",
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org")
        },
        Contact = new OpenApiContact
        {
            Name = "Barret Julien",
            Email = "dev@memos.com",
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddDbContext<MemosBdContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BDLocale")));


builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

builder.Services.AddScoped<ICompteService, CompteService>();
builder.Services.AddScoped<IMemoService, MemoService>();

var app = builder.Build();

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();