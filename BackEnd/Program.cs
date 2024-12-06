
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DI
builder.Services.AddDbContext<FruitkhaContext>(options =>
                    options.UseSqlServer(
                        builder
                        .Configuration
                        .GetConnectionString("DefaulConnection")
                        ));

builder.Services.AddDbContext<AuthDBContext>(options =>
                    options.UseSqlServer(
                        builder
                        .Configuration
                        .GetConnectionString("DefaulConnection")
                        ));

builder.Services.AddIdentityCore<IdentityUser>()
             .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("fide")
            .AddEntityFrameworkStores<AuthDBContext>()
            .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

});

builder.Services.AddAuthentication();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IImageHandler, ImageHandler>();

builder.Services.AddScoped<IProductDAL, ProductDALImpl>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryDAL, CategoryDALImpl>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
