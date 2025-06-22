using CodeiumSampleApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AddDbContext
builder.Services.AddDbContext<AppDbContext>(val => val.UseInMemoryDatabase("CodeiumSampleDB"));
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

//Seed Data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Categories.Any())
    {
        var cat1 = new CodeiumSampleApi.Models.Category { Id = 1, Name = "Electronics" };
        var cat2 = new CodeiumSampleApi.Models.Category { Id = 2, Name = "Books" };
        db.Categories.AddRange(cat1, cat2);
        db.SaveChanges();

        db.Products.AddRange(
            new CodeiumSampleApi.Models.Product { Name = "Laptop", Price = 1200, CategoryId = cat1.Id },
            new CodeiumSampleApi.Models.Product { Name = "Smartphone", Price = 800, CategoryId = cat1.Id },
            new CodeiumSampleApi.Models.Product { Name = "Novel", Price = 20, CategoryId = cat2.Id }
        );
        db.SaveChanges();
    }
}



app.Run();
