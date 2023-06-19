global using dotnet_rpg.Models;
global using dotnet_rpg.Services.CharacterService.Abstract;
global using dotnet_rpg.Services.CharacterService.Concrete;
global using dotnet_rpg.Dtos.Character;
global using Microsoft.EntityFrameworkCore;
global using dotnet_rpg.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//automapper eklemesi
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//kendi oluşturuduğumuz servisleri buildera tanıtmamız lazım
builder.Services.AddScoped<ICharacterService, CharacterService>();

//Dbcontexti ekliyoruz
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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
