using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
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
using Reglas;
using Autorizacion.Middleware;
using System.Text;

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

builder.Services.AddScoped<Abstracciones.Interfaces.DA.IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IAutenticacionRegla, AutenticacionRegla>();
builder.Services.AddScoped<IAutenticacionFlujo, AutenticacionFlujo>();
builder.Services.AddScoped<IRolDA, RolDA>();
builder.Services.AddScoped<IRolFlujo, RolFlujo>();

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
