using NLog;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


//Nlog file konfigirasyonu
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
//Controller'larý biz ayrý bir katmana aldýk, bu katmanda presentation class projesi, onu program.cs'e dahil edebilmek için AddApplicationPart dememiz gerekiyor
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database servisinin eklenmesi
//ConfigureSqlContext bizim oluþturduðumuz bir extension method, method tanýmýndaki this aslýnda buradaki builder.services
//Bizim yazdýðýmýz extensionsService'lar
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

//AutoMapper run time'da hazýrlanan bir yapý, reflection istiyor o yüzden böyle typeOf ile bu sýnýfý veriyoruz
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

//ILoggerService bizim kendi yazdýðýmýz servis
//ExceptionHandler için bir app'e ihtiyacýmýz var o yüzden app build edildikten
//sonra configure ediyoruz
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
