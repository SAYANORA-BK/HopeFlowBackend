using Infrastructure.DapperContext;
using Application.Service;
using Application.Interface.IRepository;
using Infrastructure.Repositories;
using Application.Interface.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using E_commerce.Service;

namespace Blood_donation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //.AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //})
            .AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:Key"]))
                };
            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
       


            builder.Services.AddSingleton<Dappercontext>();
            builder.Services.AddScoped<IRoleservice, Roleservice>();
            builder.Services.AddScoped<IRegisterservice, Registerservice>();
            builder.Services.AddScoped<ILoginservice,LoginService>();
            builder.Services.AddScoped<IBloodRequestservice,BloodRequestservice>();
            builder.Services.AddScoped<IRolerepository, Rolerepository>();
            builder.Services.AddScoped<IRegisterrepository, Registerrepository>();
            builder.Services.AddScoped<ILoginrepository,LoginRepository>();
            builder.Services.AddScoped<IBloodReqestRepository,BloodRequestRepository>();
            builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();
            builder.Services.AddScoped<ICertificaterepository,CertificateRepository>();



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Donor"));
                options.AddPolicy("DonorOnly", policy => policy.RequireRole("Bloodbank"));
                
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5175")
                             .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();
        



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
