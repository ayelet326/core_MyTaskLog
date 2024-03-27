using LogMiddleware.Middlewares;
using Microsoft.AspNetCore.Builder;
using UserUtils.Utilites;
using TaskLogUtils.Utilites;
using LoginUtils.Utilites;
using TokenService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
     {
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(cfg =>
     {
         cfg.RequireHttpsMetadata = true;
         cfg.TokenValidationParameters = TokenToLogin.GetTokenValidationParameters();
     });

builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User")); 
            });
builder.Services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTaskLog", Version = "v1" });
       //auth3
       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Description = "Please enter JWT with Bearer into field",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey
       });
       //auth4
       c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {}
                }
       });

       
   });
// Add services to the container.

builder.Services.AddTaskLog();
builder.Services.AddUsres();
builder.Services.AddLogin();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
 