using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Zip.WebAPI.Data;
using Zip.WebAPI.MappingProfiles;
using Zip.WebAPI.Middlewares;
using Zip.WebAPI.Repository;
using Zip.WebAPI.SeedData;
using Zip.WebAPI.Services;

namespace Zip.WebAPI
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
            services.AddDbContext<ZipUserDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserDtoMapper());
                mc.AddProfile(new AcountDtoMapper());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAcountRepository, AcountRepository>();
            services.AddScoped<IAcountService, AcountService>();
            services.AddScoped<ICreditValidator, CreditValidator>();
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ZipUserDBContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (dbContext.Users.Count() == 0)
            {
                dbContext.Database.EnsureCreated();
                dbContext.AddRange(UserDataProvider.Get());
                dbContext.SaveChanges();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseGlobalErrorHandlingMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
