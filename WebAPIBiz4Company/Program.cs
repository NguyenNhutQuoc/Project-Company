using System;
using Microsoft.EntityFrameworkCore;
using WebAPIBiz4Company.ImplementInterface.User;
using WebAPIBiz4Company.Interface.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserActivity, UserActivity>();
builder.Services.AddDbContext<WebAPIBiz4Company.Data.WEBPUBLICBETAContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("APIBiz4Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

