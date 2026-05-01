using CinemaBookingSystem.Client.Services;
using Microsoft.AspNetCore.Components.Web;                  
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CinemaBookingSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7262") });

            builder.Services.AddScoped<IMovieService, MovieService>();

            await builder.Build().RunAsync();
        }
    }
}
