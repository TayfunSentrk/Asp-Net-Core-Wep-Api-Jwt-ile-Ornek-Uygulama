using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.Configration;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));//CustomTokensOptions'dll olarak istedi�im yerde ge�ebilirim.�lgili datalar� section yap�s�ndan al�cak
builder.Services.Configure<Client>(builder.Configuration.GetSection("Clients"));//bu client herhangi bir dll constructor'da eri�mek i�in

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
