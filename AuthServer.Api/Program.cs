using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));//CustomTokensOptions'dll olarak istediðim yerde geçebilirim.Ýlgili datalarý section yapýsýndan alýcak
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));//bu client herhangi bir dll constructor'da eriþmek için
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//generic olduðu için typeof kullandýk
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Generic yapý kullandýk fakat iki tane entity alacaðý için virgül koyduk
builder.Services.AddScoped(typeof(IServiceGeneric<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IUnitofWork,UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("Asp Net Core Wep Api Jwt ile Örnek Uygulama.Data"); //burda migration assembly'data oluþturdum çünkü api assembly kirletmesin
    });
});

builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; //her bir mailin unique olmasýný saðladým
    options.Password.RequireNonAlphanumeric = false; // * ? gibi ifadeleri zorunlu olmasýný engelledim
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //þifre sýfýrlamada token üretmek için yaptým
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
