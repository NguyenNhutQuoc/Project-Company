using Microsoft.EntityFrameworkCore;
using WebAPIBiz4Company.Interface.JobType;
using WebAPIBiz4Company.Interface.User;
using WebAPIBiz4Company.Interface.Job;
using WebAPIBiz4Company.Interface.JobApplier;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJopApplierActivity, JobApplierActivity>();
builder.Services.AddScoped<IJobActivity, JobActivity>();
builder.Services.AddScoped<IUserActivity, UserActivity>();
builder.Services.AddScoped<IJobTypeActivity, JobTypeActivity>();
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

