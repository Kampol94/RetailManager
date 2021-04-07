using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RMPortalBlazorPWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent);

            await _localStorageService.SetItemAsync("authToken", result.AccessToken);

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(result.AccessToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

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
