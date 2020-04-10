using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SalesTaxCalculator
{
    public class Program
    {
        /// <summary>
        /// Entry point to the webserver program.
        /// </summary>
        /// <param name="args">Arguments to configure server.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Configure()
        {

        }
    }
}
