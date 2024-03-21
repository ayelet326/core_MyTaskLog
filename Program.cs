using LogMiddleware.Middlewares;
using Microsoft.AspNetCore.Builder;
using UserUtils.Utilites;
using TaskLogUtils.Utilites;
using LoginUtils.Utilites;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTaskLog();
builder.Services.AddUsres();
builder.Services.AddLogin();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// logFile.log תיעוד הפעולות יכתב בקובץ  
app.UseLogMiddleware("logFile.log");
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
 