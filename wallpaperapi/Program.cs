using Microsoft.EntityFrameworkCore;
using wallpaperapi.Data;
using wallpaperapi.Repository;
using wallpaperapi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WallpaperDbContext>(options => options.UseSqlServer("name=Wallpaper"));
builder.Services.AddScoped<IWallpaperRepository, WallpaperRepository>();
builder.Services.AddScoped<IAzureStorageService, AzureBlobSotrageService>();

builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
