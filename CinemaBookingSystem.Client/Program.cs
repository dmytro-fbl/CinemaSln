using CinemaBookingSystem.Client.Services;
using Microsoft.AspNetCore.Components.Web;                  
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using CinemaBookingSystem.Client.Auth;

namespace CinemaBookingSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7262") });

            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            builder.Services.AddScoped<AuthResponse>();
            

            await builder.Build().RunAsync();
        }
    }
}
