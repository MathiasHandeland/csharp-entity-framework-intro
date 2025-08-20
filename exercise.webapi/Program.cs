using exercise.webapi.Data;
using exercise.webapi.Endpoints;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Library"));

builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();

builder.Services.AddDbContext<DataContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"))
    .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    options.LogTo(message => Debug.WriteLine(message));

});

var app = builder.Build();

//using (var dbContext = new DataContext(new DbContextOptions<DataContext>()))
//{
//    dbContext.Database.EnsureCreated();
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Library API");
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.ConfigureBooksApi();

app.ConfigureAuthorApi();

app.Run();