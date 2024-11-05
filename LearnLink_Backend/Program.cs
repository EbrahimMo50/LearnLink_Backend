using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

//will not use an initializer for the database this time

//the application should have followed an inheritance logic between admin student and instructor with them sharing an abstraction, but lack of knowledge about EF Core handling denyed that :/

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
     );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
