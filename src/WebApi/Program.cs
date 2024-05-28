using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using webapi.Consts;
using webapi.Infastructure.Data;
using webapi.Infastructure.Data.Interceptors;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    
    var origins = builder.Configuration.GetValue<string>("Cors:WithOrigins")?.Split(";")?? Array.Empty<string>();
    options.AddPolicy(name: Constants.MY_ALLOW_SPECIFIC_ORIGINS,
                      policy =>
                      {
                          policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
builder.Services.AddScoped<ISaveChangesInterceptor, ProgressiveEntityInterceptor>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFastEndpoints().SwaggerDocument();
builder.Services.AddDbContext<ApplicationDbContext>((sp, o) => {
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    o.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
});
builder.Services.AddTransient<IUser, User>();

/*
    builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseInMemoryDatabase(nameof(ToDoList));
});
*/


builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseCors(Constants.MY_ALLOW_SPECIFIC_ORIGINS);

app.UseFastEndpoints().UseSwaggerGen();

app.MapGet("/", () => "Hello, World!");
    

app.Run();
