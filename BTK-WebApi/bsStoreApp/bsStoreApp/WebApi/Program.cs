using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Controller'lar� biz ayr� bir katmana ald�k, bu katmanda presentation class projesi, onu program.cs'e dahil edebilmek i�in AddApplicationPart dememiz gerekiyor
builder.Services
    .AddControllers()
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
