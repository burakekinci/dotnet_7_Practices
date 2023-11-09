using NLog;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);


//Nlog file konfigirasyonu
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
//Controller'lar� biz ayr� bir katmana ald�k, bu katmanda presentation class projesi, onu program.cs'e dahil edebilmek i�in AddApplicationPart dememiz gerekiyor
builder.Services
    .AddControllers(config =>
    {
        //Content Negatiation-��erik Pazarl���n� a�ar. (Xml,Csv format�nda d�n��ler i�in)
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
        /*Pazarl��a a����z tamam ama request'ten gelen format iste�ini reddetebiliriz
        *yani diyelim ki request "test/csv" istedi, fakat biz bu format'� kabul etmiyorsak
        *HttpNotAcceptable=true" oldu�undan controller otomatik 406 not acceptable d�ner
        *Pazarl��a a��k m�y�z evet, duruma g�re not acceptable d�nebilir miyiz evet
        */
    })
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database servisinin eklenmesi
//ConfigureSqlContext bizim olu�turdu�umuz bir extension method, method tan�m�ndaki this asl�nda buradaki builder.services
//Bizim yazd���m�z extensionsService'lar
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

//AutoMapper run time'da haz�rlanan bir yap�, reflection istiyor o y�zden b�yle typeOf ile bu s�n�f� veriyoruz
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

//ILoggerService bizim kendi yazd���m�z servis
//ExceptionHandler i�in bir app'e ihtiyac�m�z var o y�zden app build edildikten
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
