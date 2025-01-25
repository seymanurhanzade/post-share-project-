using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using react.server.ApplicationContext;
using react.server.DataManagement.server.business.Abstract;
using react.server.DataManagement.server.business.Concrete;
using react.server.DataManagement.server.data.Abstract;
using react.server.DataManagement.server.data.Concrate;
using server.business.Abstract;
using server.business.Concrete;
using server.data.Abstract;
using server.data.Concrate;


namespace react.server
{
    public class Startup
    {
        public IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("Policy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));


            services.AddIdentity<Users, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();



            services.AddScoped<IAuthRepository, ConcrateAuthRepository>();
            services.AddScoped<IInstantShareRepository, ConcrateInstantShareRepository>();
            services.AddScoped<IUserRepository, ConcrateUserRepository>();
            services.AddScoped<IFollowingRepository, ConcrateFollowingRepository>();
            services.AddScoped<IAdminRepository, ConcrateAdminRepository>();

            services.AddScoped<IAuthService, AuthManager>();  
            services.AddScoped<IInstantShareService, InstantShareManager>();
            services.AddScoped<IUserService, UserManager>();  
            services.AddScoped<IFollowingService, FollowingManager>(); 
            services.AddScoped<IAdminService, AdminManager>(); 

            // services.AddCookiePolicy
            services.AddRazorPages();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAuthorization();
            
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager,IdentityContext context)
        {
            if (env.IsDevelopment())
            {
                SeedIdentity.Seed(userManager, roleManager, configuration, context).Wait();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "react.app", "public", "Images")),
                RequestPath = "/Images"
            });

            app.UseRouting();
            app.UseCors("Policy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}