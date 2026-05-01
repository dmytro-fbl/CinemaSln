using System.Net.Http.Json;
using Blazored.LocalStorage;
using CinemaBookingSystem.Client.Auth;
using CinemaBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;


namespace CinemaBookingSystem.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if(result != null && !string.IsNullOrEmpty(result.Token))
                {
                    await _localStorage.SetItemAsync("authToken", result.Token);

                    ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Token);
                    return true;
                }
            }
            return false;
        }

        public async Task Logout()
        {
            ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        }
        
    }
}

public class AuthResponse
{
    public string Token { get; set; }
}
