using _34375309_Project2.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using _34375309_Project2.Models;
using System.Text;

namespace _34375309_Project2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<HSProjectdbdevContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddDbContext<AppDBContext_Auth>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Project 2, 34375309",
                    Version = "v2"
                }

                );
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                }
); c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                {
                                new OpenApiSecurityScheme {
                                    Reference=new OpenApiReference {
                                        Type=ReferenceType.SecurityScheme,
                                        Id="Bearer"
                                    }}, new string[] {} } });
            });

            //services.AddDbContext<AppDBContext_Auth>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // For Identity  
            services.AddIdentity<AppUser_Auth,
            IdentityRole>().AddEntityFrameworkStores<AppDBContext_Auth>().AddDefaultTokenProviders();
            // Add Authentication  
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ) 
            // Adding Jwt Bearer  
            .AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            }
            );
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "CMPG322-Project 2, 34375309"));
        }
    }
}
