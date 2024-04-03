using BookRental.DAL;
using BookRental.Model;
using BookRental.WebApi.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BookRental.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure DbContext
            services.AddDbContext<BookRentalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BookRentalConnectionString")));

            // Configure JWT authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSettings:SecretKey"]);
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddTransient<IRepository<User>, CommonRepository<User>>();
            services.AddTransient<IRepository<Book>, CommonRepository<Book>>();
            services.AddTransient<IRepository<BorrowedBook>, CommonRepository<BorrowedBook>>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Add your frontend URL here
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireUserRole", policy =>
                    policy.RequireRole("User", "Admin")); // Specify roles needed
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowOrigin");


            // Use authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
