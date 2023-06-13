using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using WebApi.Hash;
using WebApi.Helpers;
using WebApi.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddTransient<IHash, MD5Hash>();
// builder.Services.Configure<ApiBehaviorOptions>(options
//     => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<ICategoryService, CategoryManager>();
builder.Services.AddSingleton<IProductService, ProductManager>();

builder.Services.AddSingleton<IUserDal, EfUserDal>();
builder.Services.AddSingleton<ICategoryDal, EfCategoryDal>();
builder.Services.AddSingleton<IProductDal, EfProductDal>();


var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// builder.Services.AddDbContext<StockTrackingContext>(options =>
// {
//     var connectionString = "server=localhost;port=3306;database=StockTracking;user=local;password=123456";
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
// });


var appSettings = appSettingsSection.Get<AppSettings>();
byte[] key = new byte[20];
if (appSettings != null)
{
    key = Encoding.UTF8.GetBytes(appSettings.Secret);
}

//Console.WriteLine("Secret : " + appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.UseCors();


string imagegesPath = Path.Combine(Directory.GetCurrentDirectory(), @"images");

if (!Directory.Exists(imagegesPath))
{
    Directory.CreateDirectory(imagegesPath);
}

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(imagegesPath),
    RequestPath = new PathString("/app-images")
});

app.MapControllers();

app.Run();
