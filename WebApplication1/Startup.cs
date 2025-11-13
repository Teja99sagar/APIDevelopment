using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicApi.Data;
namespace MusicApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Music API", Version = "v1" });
            });
            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
            // Additional service configurations can be added here
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseSwagger();
        //        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApi v1"));
        //    }

        //dbContext.Database.EnsureCreated();
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApi v1"));
            }

            // 🔧 Apply migrations automatically
            //try {
            //    dbContext.Database.Migrate();

            //}
            //catch (Exception ex)
            //{
            //    // Log the exception (you can use any logging framework)
            //    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoint
                =>
            {
                endpoint.MapControllers();


            });
        }
    }
}
