using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            var contextOptions = new DbContextOptionsBuilder<ZipUserDBContext>()
                .UseSqlite(Configuration.GetConnectionString("DefaultConnection")).Options;

            var dbContext = new ZipUserDBContext(contextOptions);
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            dbContext.Database.OpenConnection();
            dbContext.Users.AddRange(UserDataProvider.Get());
            dbContext.SaveChanges();

            services.AddSingleton<ZipUserDBContext>(dbContext);

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
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
