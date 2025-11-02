using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using WebApplication1;

namespace MusicApi

{
    public class Program

    {
        public static void Main(string[] args)
        {
            CreatHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreatHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                {
                    webBuilder.UseStartup<Startup>();
                }
            });

    }

}