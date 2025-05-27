using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Models.Validators;
using EncuestasAPIU3_4.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("EncuestasCS");

builder.Services.AddDbContext<EncuestaContext>(x =>
x.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddScoped(typeof(Repository<>),typeof(Repository<>));
builder.Services.AddScoped(typeof(UsuarioValidator));


builder.Services.AddControllers();
builder.Services.AddCors(x =>
{
    x.AddPolicy("todos", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Encuestas API",
        Version = "v1",
        Description = "API para gestionar encuestas escolares"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("todos");

app.UseFileServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
