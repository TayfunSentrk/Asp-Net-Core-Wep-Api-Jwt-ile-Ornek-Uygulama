using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.Services;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Core.UnitOfWork;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Data;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Data.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_�rnek_Uygulama.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));//CustomTokensOptions'dll olarak istedi�im yerde ge�ebilirim.�lgili datalar� section yap�s�ndan al�cak
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));//bu client herhangi bir dll constructor'da eri�mek i�in
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//generic oldu�u i�in typeof kulland�k
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Generic yap� kulland�k fakat iki tane entity alaca�� i�in virg�l koyduk
builder.Services.AddScoped(typeof(IServiceGeneric<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IUnitofWork,UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("Asp Net Core Wep Api Jwt ile �rnek Uygulama.Data"); //burda migration assembly'data olu�turdum ��nk� api assembly kirletmesin
    });
});

builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; //her bir mailin unique olmas�n� sa�lad�m
    options.Password.RequireNonAlphanumeric = false; // * ? gibi ifadeleri zorunlu olmas�n� engelledim
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //�ifre s�f�rlamada token �retmek i�in yapt�m
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
