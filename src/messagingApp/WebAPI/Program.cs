using Persistence;
using Application;
using Infrastructure;
using WebAPI;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//await context.Database.MigrateAsync();

//if(!app.Environment.IsDevelopment())
app.ConfigureCustomExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
