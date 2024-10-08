using LAB7.DATA.Models;
using LAB7.DATA.Repositories.IRepositories;
using LAB7.DATA.Repositories.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppAPIDbContext>(options =>
{

});
builder.Services.AddScoped<IRepositoriesAppAPI<ChucVu>,ChucVuRepositories>();
builder.Services.AddScoped<IRepositoriesAppAPI<Employee>,EmployeeRepositories>();
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
