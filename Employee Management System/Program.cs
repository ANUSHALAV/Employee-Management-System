using Employee_Management_System.Models;
using Employee_Management_System.Services.Implementations;
using Employee_Management_System.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DataBaseSettings"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<DbSettings>>().Value);

//services Registration..
builder.Services.AddScoped<IEmployeServices, EmployeServices>();
builder.Services.AddScoped<IMasterServices, MasterServices>();  

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
