using LearnLink_Backend.MiddleWares;
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
using LearnLink_Backend.Entities;
using LearnLink_Backend.Repositories.AnnouncementsRepo;
using LearnLink_Backend.Repositories.CoursesRepo;
using LearnLink_Backend.Repositories.MeetingsRepo;
using LearnLink_Backend.Repositories.PostsRepo;
using LearnLink_Backend.Repositories.SchedulesRepo;
using LearnLink_Backend.Repositories.SessionsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;
using LearnLink_Backend.Services.AdminstrationsService;
using LearnLink_Backend.Services.AnnouncementsService;
using LearnLink_Backend.Services.AuthService;
using LearnLink_Backend.Services.CoursesService;
using LearnLink_Backend.Services.JWTService;
using LearnLink_Backend.Services.MeetingsService;
using LearnLink_Backend.Services.PostsService;
using LearnLink_Backend.Services.SessionsService;
using LearnLink_Backend.Services.UsersService;

//will not use an initializer for the database this time if needed will use the way of intializing in the AppDbContext class on model creation will add records

//database uses TBC(table per conc) for each type of user
//such implementation limit scalability and checking the user login will occur on 3 tables adding more roles will require more checks further look up for a better method should be carried

//the app will follow the failfast design no try catches will be used to stress the program for bugs and potential errors

//there is no relation between the admin and the creater foreign key
//to fix this we need to know how to use One-to-one without navigation to dependent to prevent cascading cycles

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
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
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();     //all other repos are depndent on it handy to make it singleton and reduce redundancy
builder.Services.AddSignalR();

// policies injection
builder.Services.AddScoped<IAuthorizationHandler, StudentRequirmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdminRequirmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, InstructorRequirmentHandler>();

// auth releated injections
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// modules structure injections
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService ,UserService>();
builder.Services.AddScoped<IAdminstrationService, AdministrationService>();

builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IInstructorScheduleRepo, InstructorScheduleRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<IMeetingRepo, MeetingRepo>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<IAnnouncementRepo, AnnouncementRepo>();

// independent services injections
builder.Services.AddDbContext<AppDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
     );
builder.Services.AddScoped<MediaService>();


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

app.UseMiddleware<ExceptionHandlingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// CARE TO TEST THE APP THROUGHLY THE LAST REFACTOR WAS SO HEAVY BUGS MAY HAVE SLIPT THROUGH
// create notification system with signalR