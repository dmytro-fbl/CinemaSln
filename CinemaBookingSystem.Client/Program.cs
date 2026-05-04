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

            builder.Services.AddTransient<CustomAuthorizationHandler>();

            builder.Services.AddScoped(sp =>
            {

                var handler = sp.GetRequiredService<CustomAuthorizationHandler>();
                handler.InnerHandler = new HttpClientHandler();
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7262/")
                };
            }); 

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();            

            await builder.Build().RunAsync();
        }
    }
}
