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
using Infrastructure.SignalR;

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


            builder.Services.AddSignalR();





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
            builder.Services.AddScoped<IAdminRepository,AdminRepository>();
            builder.Services.AddScoped<IAdminService,AdminService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IBloodBankRepository, BloodBankRepository>();    
            builder.Services.AddScoped<IBloodBankService, BloodBankService>();
            builder.Services.AddScoped<IDonorRepository, DonorRepository>();
            builder.Services.AddScoped<IDonorService, DonorService>();



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
                options.AddPolicy("DonorOnly", policy => policy.RequireRole("Donor"));
                options.AddPolicy("RecipientOnly", policy => policy.RequireRole("Recipient/Hospital"));
                options.AddPolicy("BloodbankOnly", policy => policy.RequireRole("Bloodbank"));
                
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                             .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            app.MapHub<NotificationHub>("/notificationHub");



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseMiddleware<GetUserIdMiddleWare>();
            app.MapControllers();

            app.Run();
        }
    }
}
