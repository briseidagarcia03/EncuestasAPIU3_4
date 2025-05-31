using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Models.Validators;
using EncuestasAPIU3_4.Repositories;
using EncuestasAPIU3_4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
{
    jwtOptions.Audience = builder.Configuration["Jwt:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = true
    };
});

var cs = builder.Configuration.GetConnectionString("EncuestasCS");

builder.Services.AddDbContext<EncuestaContext>(x =>
x.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddScoped(typeof(Repository<>),typeof(Repository<>));
builder.Services.AddTransient<JwtService>();

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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingresa el token en el formato: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
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
            new string[] { }
        }
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
