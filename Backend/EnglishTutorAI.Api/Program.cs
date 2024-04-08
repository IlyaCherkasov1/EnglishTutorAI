using EnglishTutorAI.Application;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Profiles;
using EnglishTutorAI.Application.Services;
using EnglishTutorAI.Application.Services.Sentences;
using EnglishTutorAI.Infrastructure;
using EnglishTutorAI.Infrastructure.Data;
using EnglishTutorAI.Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<OpenAiConfig>(builder.Configuration.GetSection(nameof(OpenAiConfig)));
builder.Services.Configure<ProxyConfig>(builder.Configuration.GetSection(nameof(ProxyConfig)));
builder.Services.AddScoped<ISentenceRetrieverService, SentenceRetrieverService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddSingleton<IHttpClientFactory, ConfigurableProxyHttpClientFactory>();
builder.Services.AddScoped<IPromptTemplateService, PromptTemplateService>();
builder.Services.AddScoped<IStoryRetrieverService, StoryRetrieverService>();

builder.Services.AddApplicationDependencies();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)));
});

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));
var app = builder.Build();

app.UseCors(
    options => options.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!)
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();