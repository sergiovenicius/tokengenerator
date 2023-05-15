using common.Mapper;
using common.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using tokengenerator.Database;
using tokengenerator.Model;
using tokengenerator.Repository;
using tokengenerator.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddDbContext<DBCardContext>(o => o.UseInMemoryDatabase(databaseName: "dbcard"));

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IMapper<DBCard, CardInput>, MapperCardInputToDBCard>();
builder.Services.AddScoped<IMapper<CardResponse, DBCard>, MapperDBCardToCardResponse>();

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

app.Run("http://0.0.0.0:5000");
