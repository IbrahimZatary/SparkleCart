using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SparkeApp.Data;
using SparkeApp.Services;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddControllers();
// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
// Services DI 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();




builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SparkleCart  E-Commerce API ",
        Version = "v1"
    });
});


builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();