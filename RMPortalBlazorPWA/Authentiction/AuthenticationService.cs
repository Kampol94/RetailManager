using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RMPortalBlazorPWA.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMPortalBlazorPWA.Authentiction
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationUserModel)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type", "password"),
                new KeyValuePair<string,string>("username", authenticationUserModel.Email),
                new KeyValuePair<string,string>("password", authenticationUserModel.Password),
            });

            var authResult = await _httpClient.PostAsync("https://localhost:44305/token", data);

            var token = await _localStorageService.GetItemAsync<string>("authToken");

            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            await _localStorageService.SetItemAsync("authToken", result.Access_Token);

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(result.Access_Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

            return result;
        }

        public async Task LogOut()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
        }
    }
}
