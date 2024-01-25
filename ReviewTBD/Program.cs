using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Startup;
using System.Text.Json.Serialization;
using ReviewTBDAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReviewContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.RegisterServices();

builder.Services
    .AddControllers()
    .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
