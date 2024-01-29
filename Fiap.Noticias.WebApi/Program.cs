using Fiap.Autenticacao.Core.ConfiguracaoFila;
using Fiap.Autenticacao.Core.Mediatr;
using Fiap.Noticias.WebApi.CommandsQueries;
using Fiap.Noticias.WebApi.CommandsQueries.ValidationClasses;
using Fiap.Noticias.WebApi.Data.Context;
using Fiap.Noticias.WebApi.Data.Repositories;
using Fiap.Noticias.WebApi.HttpServices;
using Fiap.Noticias.WebApi.Model.RepositoryInterfaces;
using Fiap.Noticias.WebApi.Services;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<NoticiaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NoticiaConnectionString"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRequestHandler<CriarAutorCommand, ValidationResult>, NoticiasCommandHandler>();
builder.Services.AddScoped<IMediatrHandler, MediatorHandler>();
builder.Services.AddScoped<NoticiaDbContext>();

builder.Services.AddSingleton<IFilaMensagem>(new FilaMensagem()).AddHostedService<CriarAutorFilaHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
