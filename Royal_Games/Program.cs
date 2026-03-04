using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Options;
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Royal_Games.Applications.Autenticacao;
using Royal_Games.Applications.Services;
using Royal_Games.Contexts;
using Royal_Games.Interfaces;
using Royal_Games.Repositories;
<<<<<<< HEAD
using System.Text;

=======
using System;
using System.Text;
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
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

;

builder.Services.AddDbContext<Royal_GamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(default)));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<JogoService>();


builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

<<<<<<< HEAD
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<JogoService>();

=======
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando JWT.
    .AddJwtBearer(options =>
    {

        var chave = builder.Configuration["Jwt:Key"]!;


        var issuer = builder.Configuration["Jwt:Issuer"]!;

<<<<<<< HEAD
 
        var audience = builder.Configuration["Jwt:Audience"]!;

 
=======

        var audience = builder.Configuration["Jwt:Audience"]!;


>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,

<<<<<<< HEAD
         
            ValidateAudience = true,

            
            ValidateLifetime = true,

          
            ValidateIssuerSigningKey = true,

            
            ValidIssuer = issuer,

            
=======

            ValidateAudience = true,


            ValidateLifetime = true,


            ValidateIssuerSigningKey = true,


            ValidIssuer = issuer,


>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
            ValidAudience = audience,


            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            )
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
