using EnglishTutorAI.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Services.InstallServicesInAssembly(builder.Configuration);

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