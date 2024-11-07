using LearnLink_Backend.Repostories.UserRepos;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

//will not use an initializer for the database this time

//the application should have followed an inheritance logic between admin student and instructor with them sharing an abstraction, but lack of knowledge about EF Core handling denyed that :/
//such implementation will limit scalability and checking the user login will occur on 3 tables adding more roles will require more checks further look up for a better method should be carried

//the app will follow the failfast design no try catches will be used to stress the program for bugs and potential errors

//there is no relation between the admin and the creater foreign key

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
     );
builder.Services.AddScoped<IUserRepo,UserRepo>();
builder.Services.AddScoped<Authentaction>();

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
