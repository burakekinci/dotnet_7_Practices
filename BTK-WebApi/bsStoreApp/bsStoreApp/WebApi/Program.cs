using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

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
