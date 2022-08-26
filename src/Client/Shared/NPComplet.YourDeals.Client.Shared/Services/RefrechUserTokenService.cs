using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Radzen;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public class RefrechUserTokenService : IRefrechUserTokenService
    {

        private IHttpClientService _httpClientService;
     

        private volatile int _NumberOfExecution = 0;

       

        public int NumberOfExecution
        {
            get { return _NumberOfExecution; }
        }

        private volatile bool IsRunning;

        public RefrechUserTokenService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
           

          

        }

        public async Task<string> RefreshToken()
        {
            var token = await NPCompletApp.ShellViewModel.GetStorage<string>("authToken");
            var refreshToken = await NPCompletApp.ShellViewModel.GetStorage<string>("refreshToken");
            var tokenDto = JsonSerializer.Serialize(new RefreshTokenDto { Token = token, RefreshToken = refreshToken });
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");

            var refreshResult = await _httpClientService.ApplicationHttpClient.PostAsync(Config.RefreshToken, bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(refreshContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!refreshResult.IsSuccessStatusCode)
            {
                await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error", "Something went wrong during the refresh token action");
                return string.Empty;
            }
            await NPCompletApp.ShellViewModel.SetStorage("authToken", result.Data.Token);
            await NPCompletApp.ShellViewModel.SetStorage("refreshToken", result.Data.RefreshToken);

            _httpClientService.ApplicationHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Data.Token);
            return result.Data.Token;
        }



        public async Task DoWork(CancellationToken CancellationToken)
        {
            if (IsRunning)
                return;
            IsRunning = true;
           
                
                _ = Interlocked.Increment(ref _NumberOfExecution);
                await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Info, "Info", $"NumberOfExecution is {NumberOfExecution}");
                await Task.Delay(5000);
                Console.WriteLine($"RefrechUserTokenService {NumberOfExecution}");
                
                var user = NPCompletApp.ShellViewModel?.UserState?.User;
                var exp = user?.FindFirst(c => c.Type.Equals("exp")).Value;
                if (exp != null)
                {
                    var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
                    var timeUTC = DateTime.UtcNow;
                    var diff = expTime - timeUTC;
                    if (diff.TotalSeconds <= 1)
                    {
                        var newtoken = await RefreshToken();
                    await NPCompletApp.ShellViewModel.GetUserStatus();
                        if (!string.IsNullOrEmpty(newtoken))
                        {

                            _httpClientService.ApplicationHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", newtoken);
                            await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Info, "Info", "Token is Refreshed");
                        }
                    }
                    else
                    {
                        await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Info, "Info", $"TotalSeconds is {diff.TotalSeconds}");
                    }

                }
                else
                {
                    await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error", "Could not refresh the use token ");
                }

            IsRunning = false;

            
        }
    }
}
