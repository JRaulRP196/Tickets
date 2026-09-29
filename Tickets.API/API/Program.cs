using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Autorizacion.Abstracciones.Interfaces.DA;
using Autorizacion.Abstracciones.Interfaces.Flujo;
using Autorizacion.DA;
using Autorizacion.Flujo;
using DA;
using DA.Repositorios;
using Flujo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Autorizacion.Middleware;
using System.Text;
using Abstracciones.Interfaces.Reglas;
using Reglas;
using Abstracciones.Interfaces.Servicios;
using Servicios;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var tokenConfiguracion = builder.Configuration.GetSection("TokenConfiguracion").Get<TokenConfiguracion>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguracion.Issuer,
        ValidAudience = tokenConfiguracion.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguracion.Key))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<Abstracciones.Interfaces.DA.IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<ITicketDA, TicketDA>();
builder.Services.AddScoped<ITicketFlujo, TicketFlujo>();
builder.Services.AddScoped<ITicketRegla, TicketRegla>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.AddScoped<IUsuarioServicios, UsuarioServicio>();

builder.Services.AddTransient<IAutorizacionFlujo, AutorizacionFlujo>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.Interfaces.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAutorizacionMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
