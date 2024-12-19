using LearnLink_Backend.Modules.Adminstration;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Modules.Authentcation;
using LearnLink_Backend.Modules.Courses;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Modules.Meeting;
using LearnLink_Backend.Modules.Meeting.Repos;
using LearnLink_Backend.Modules.Session;
using LearnLink_Backend.Modules.Session.Repos;
using LearnLink_Backend.Modules.User.Services;
using LearnLink_Backend.Policies.AdminPolicy;
using LearnLink_Backend.Policies.InstructorPolicy;
using LearnLink_Backend.Policies.StudentPolicy;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

//will not use an initializer for the database this time if needed will use the way of intializing in the AppDbContext class on model creation will add records

//the application should have followed an inheritance logic between admin student and instructor with them sharing an abstraction, but lack of knowledge about EF Core handling denyed that :/
//such implementation will limit scalability and checking the user login will occur on 3 tables adding more roles will require more checks further look up for a better method should be carried

//the app will follow the failfast design no try catches will be used to stress the program for bugs and potential errors

//there is no relation between the admin and the creater foreign key
//to fix this we need to know how to use One-to-one without navigation to dependent to prevent cascading cycles

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "JWT Authentication",
            Description = "Enter your JWT token in this field",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        c.AddSecurityDefinition("Bearer", securityScheme);

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        };

        c.AddSecurityRequirement(securityRequirement);
    });


// .NET libs dependecny injection 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();     //all other repos are depndent on it handy to make it singleton
builder.Services.AddDbContext<AppDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
     );

// policies injection
builder.Services.AddScoped<IAuthorizationHandler, StudentRequirmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdminRequirmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, InstructorRequirmentHandler>();

// auth releated injections
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<TokenService>();

// modules structure injections
builder.Services.AddScoped<AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepo, AnnouncementRepo>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<MeetingService>();
builder.Services.AddScoped<IMeetingRepo, MeetingRepo>();
builder.Services.AddScoped<AdministrationService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<UserService>();

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
        var PrivateKey = config.GetSection("PrivateKey").Value;

        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }); builder.Services.AddAuthorizationBuilder()
    .AddPolicy("StudentPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new StudentRequirment());
    })
    .AddPolicy("InstructorPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new InstructorRequirment());
    })
    .AddPolicy("AdminPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new AdminRequirment());
    })
    .AddPolicy("AdminOrInstructor", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin", "Instructor");
    })
    .AddPolicy("User", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.Role));
    });

var app = builder.Build();

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

// TODO next test the app importantly because the system was not tested at all
// add option for users to apply for instructor in user module add range on days and hours in meetings