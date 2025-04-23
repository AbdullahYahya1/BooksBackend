using BooksBackend.BusinessLayer.IService;
using BooksBackend.BusinessLayer.Services;
using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.IRepositories;
using BooksBackend.DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

public static class DependencyInjection
{

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BooksDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection Services)
    {
        Services.AddScoped<IAuthService, AuthService>();
        Services.AddScoped<IBookService,BookService>();
        Services.AddScoped<IFollowService,FollowService>();
        Services.AddScoped<IListService,ListService>();
        Services.AddScoped<IReviewService,ReviewService>();
        Services.AddScoped<IUserService, UserService>();

        Services.AddHttpClient();
        Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return Services; 
    }

    public static IServiceCollection AddRepositorys(this IServiceCollection Services)
    {
        Services.AddScoped<IUnitOfWork, UnitOfWork>();
        Services.AddScoped<IUserRepository, UserRepository>();
        Services.AddScoped<IBookRepository, BookRepository>();
        Services.AddScoped<IReviewRepository, ReviewRepository>();
        Services.AddScoped<IUserFollowRepository, UserFollowRepository>();
        Services.AddScoped<IUserListRepository, UserListRepository>();
        Services.AddScoped<IUserReadBookRepository, UserReadBookRepository>();
        Services.AddScoped<IGenreRepository, GenreRepository>();
        Services.AddScoped<IBookGenreRepository, BookGenreRepository>();

        return Services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection Services)
    {
        Services.AddControllers();

        Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIs", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.  
                          Enter 'Bearer' [space] example : 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                    {new OpenApiSecurityScheme{
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()}
                });
        });
        return Services;
    }


    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            };
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                policyBuilder => policyBuilder.WithOrigins(
                                                "http://localhost:5173"
                                              )
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials());
        });
        return services;
    }

}
