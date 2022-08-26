using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Extensions;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.LocationsUI;
using NPComplet.YourDeals.Client.Shared.WebUI.Shared;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels;
using NPComplet.YourDeals.Domain.Shared.Shared;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity
{
    public class GetProfileVM : ViewModelBase
    {
        private readonly HttpClient _httpClient;

        private readonly NavigationManager navMan;

        private readonly JsonSerializerOptions _options;





        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileVM"/> class.
        ///     Initialize new instance of <see cref="GetProfileVM"/>
        /// </summary>
        /// <param name="httpClient">
        /// The HTTP client.
        /// </param>
        public GetProfileVM(NavigationManager navMan, HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.navMan = navMan;
            this._options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            User = new User();
        HubConnection= new HubConnectionBuilder()
                .WithUrl(Config.BaseUrl + ApplicationConstants.SignalR.PingRequest, options => {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                    options.Headers.Add("Authorization", $"Bearer {NPCompletApp.Token}");
                    options.AccessTokenProvider = async () => await Task.FromResult(NPCompletApp.Token);

                })
                .WithAutomaticReconnect().Build();
            if (this.HubConnection.State == HubConnectionState.Disconnected) Task.Run(this.StartHub);
            Addressess = new List<Address>();

        }
        private async Task StartHub()
        {
            await this.HubConnection.StartAsync();
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public List<Address> Addressess { get; set; }



        public string profileImgInBase64 { get; set; }

        /// <summary>
        ///     Get All Users
        /// </summary>
        /// <returns>Users.</returns>
        public async Task GetUser()
        {
            NPCompletApp.ShellViewModel.isBusy = true;

            //var deserializedResult = await this._httpClient.GetFromJsonAsync<Result<User>>(Config.GetUser);


            var result = await this._httpClient.GetAsync(Config.GetUser);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result<User>>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {
                this.User = deserializedResult.Data;


                profileImgInBase64 = "data:image/"
                    + $"{deserializedResult.Data.Profile.ProfileImage.FileExtension};"
                    + "base64, "
                    + Convert.ToBase64String(deserializedResult.Data.Profile.ProfileImage.Data);



                NPCompletApp.ShellViewModel.isBusy = false;

            }


        }

        public async Task GetTheUserAddresses()
        {

            NPCompletApp.ShellViewModel.isBusy = true;



            var result = await this._httpClient.GetAsync(Config.GetTheUserAddresses);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result<List<Address>>>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {
                this.Addressess = deserializedResult.Data;

                NPCompletApp.ShellViewModel.isBusy = false;

            }


        }

        public async Task DeleteAddress(Address address)
        {
            var contentSerialize = JsonSerializer.Serialize(address);
            var bodyContent = new StringContent(contentSerialize, Encoding.UTF8, "application/json");


            var result = await _httpClient.PostAsync(Config.DeleteAddress, bodyContent);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {

                NPCompletApp.ShellViewModel.isBusy = false;

            }

        }

        public async Task UpdateAddress(Address address)
        {
            try
            {
                var contentSerialize = JsonSerializer.Serialize(address);
                var bodyContent = new StringContent(contentSerialize, Encoding.UTF8, "application/json");


                var result = await _httpClient.PostAsync(Config.UpdateAddress, bodyContent);
                var content = await result.Content.ReadAsStringAsync();

                var deserializedResult = JsonSerializer.Deserialize<Result<Address>>(content, _options);

                if (!deserializedResult.Succeeded)
                {
                    NPCompletApp.ShellViewModel.isLoggedIn = false;
                    NPCompletApp.ShellViewModel.isBusy = false;
                    navMan.NavigateTo("/", true);
                }
                else
                {
                    _ = deserializedResult.Data;
                    NPCompletApp.ShellViewModel.isBusy = false;

                }
            }
            catch (Exception)
            {
                //Code for exception
            }


        }

        public async Task AddNewAddress(Address address)
        {
            var contentSerialize = JsonSerializer.Serialize(address);
            var bodyContent = new StringContent(contentSerialize, Encoding.UTF8, "application/json");


            var result = await _httpClient.PostAsync(Config.AddNewAddress, bodyContent);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result<Address>>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {
                _ = deserializedResult.Data;
                NPCompletApp.ShellViewModel.isBusy = false;

            }
        }


        public async Task OnUserChanged(User user)
        {
            var contentSerialize = JsonSerializer.Serialize(user);
            var bodyContent = new StringContent(contentSerialize, Encoding.UTF8, "application/json");


            var result = await _httpClient.PostAsync(Config.UpdateUser, bodyContent);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result<User>>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {
                _ = deserializedResult.Data;
                NPCompletApp.ShellViewModel.isBusy = false;

            }
        }

        public async Task OnProfileImageChange(MultipartFormDataContent multipartFormDataContent)
        {

            this._httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", NPCompletApp.Token);

            var result = await _httpClient.PostAsync(Config.UploadUserProfileImg, multipartFormDataContent);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result>(content, _options);

            if (!deserializedResult.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                NPCompletApp.ShellViewModel.isBusy = false;
                navMan.NavigateTo("/", true);
            }
            else
            {
                NPCompletApp.ShellViewModel.isBusy = false;

            }

        }


        public async Task<ContactDto> GetContactDto(string userId)
        {
            this._httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", NPCompletApp.Token);

            var bContent = JsonSerializer.Serialize(userId);
            var bodyContent = new StringContent(bContent, Encoding.UTF8, "application/json");


            var result = await _httpClient.PostAsync(Config.GetContactWithUserId, bodyContent);
            var content = await result.Content.ReadAsStringAsync();

            var deserializedResult = JsonSerializer.Deserialize<Result<ContactDto>>(content, _options);
            return deserializedResult.Data;

        }





        public async Task ExecuteDeleteUser()
        {

            NPCompletApp.ShellViewModel.isBusy = true;
            await this._httpClient.DeleteAsync(Config.DeleteUser);
            NPCompletApp.ShellViewModel.isBusy = false;
            this.navMan.NavigateTo("/login", true);

        }

        private List<Message> _messages;
        [Parameter]
        public List<Message> messages
        {
            get { return _messages; }
            set { _messages = value;
           
                OnParametersSetAsync();
            }
        }


        /// <summary>
        /// Gets or sets the on item chose.
        /// </summary>
        [Parameter]
        public EventCallback<Message> OnItemChose { get; set; }

        public HubConnection HubConnection { get; set; }

        public List<User> contacts { get; set; } = new List<User>();

        public List<User> onlineContacts { get; set; } = new List<User>();

        public List<User> offlineContacts { get; set; } = new List<User>();

        public User user = new User() { Profile = new Profile() { Address = new Domain.Shared.Shared.Address() } };
        public List<Address> addresses = new List<Address>();

        public Address newAddress { get; set; } = null;

        public string profilePicBase64 = "";
       
        
        public override async Task OnInitializedAsync()
        {
            newAddress = null;
            await GetUser();
            await GetTheUserAddresses();

            this.user = User;
            this.profilePicBase64 = profileImgInBase64;
            this.addresses = Addressess;

            this.StateHasChanged();
         



            HubConnection.On<string>(Config.BaseUrl + ApplicationConstants.SignalR.PingRequest, async (userId) =>
            {
                await handlePingResponse(userId);
            });
        }

        public override async Task OnParametersSetAsync()
        {
            try
            {
                foreach (Message msg in messages)
                {
                    if (!NPCompletApp.ShellViewModel.UserState.User.GetUserId().Contains(msg.ToUserId.ToString()))
                    {
                        if (contacts.Find(x => x.Id == msg.ToUser.Id) == null)
                        {
                            contacts.Add(msg.ToUser);

                        }

                    }
                    else
                    {
                        if (contacts.Find(x => x.Id == msg.Owner.Id) == null)
                        {
                            contacts.Add(msg.Owner);

                        }

                    }

                }

                offlineContacts = contacts;
                var userId = NPCompletApp.ShellViewModel.UserState.User.GetUserId();

                await HubConnection.SendAsync(Config.BaseUrl + ApplicationConstants.SignalR.PingRequest, userId);

                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromSeconds(20);

                var timer = new System.Threading.Timer(async (e) =>
                {
                    StateHasChanged();

                    foreach (Message msg in this.messages)
                    {
                        if (!NPCompletApp.ShellViewModel.UserState.User.GetUserId().Contains(msg.ToUserId.ToString()))
                        {
                            if (contacts.Find(x => x.Id == msg.ToUser.Id) == null)
                            {
                                contacts.Add(msg.ToUser);
                            }

                        }
                        else
                        {
                            if (contacts.Find(x => x.Id == msg.Owner.Id) == null)
                            {
                                contacts.Add(msg.Owner);
                            }

                        }

                    }

                    offlineContacts = contacts;
                    onlineContacts = new List<User>();

                    await HubConnection.SendAsync(Config.BaseUrl + ApplicationConstants.SignalR.PingRequest, userId);

                }, null, startTimeSpan, periodTimeSpan);
            }
            catch (Exception ex)
            {

            
            }
         

        }



        public async Task handlePingResponse(string userId)
        {

            User selectedUser = contacts.Find(x => x.Id.ToString() == userId);
            if (selectedUser != null)
            {
                if (offlineContacts.Find(x => x.Id == selectedUser.Id) != null)
                {
                    onlineContacts.Add(selectedUser);
                    offlineContacts.Remove(selectedUser);
                }
                StateHasChanged();
            }
        }


        public string convertImgToBase64(User user)
        {
            string Base64 =
            "data:image/"
                    + $"{user.Profile.ProfileImage.FileExtension};"
                    + "base64, "
                    + Convert.ToBase64String(user.Profile.ProfileImage.Data);

            return Base64;
        }

        /// <summary>
        /// The choose async.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
       public Task ChooseAsync(Message msg)
        {
            StateHasChanged();
            return OnItemChose.InvokeAsync(msg);
        }
        public async Task OnUserChange(User user)
        {
            await OnUserChanged(user);
            await OnInitializedAsync();
            this.StateHasChanged();
        }

        public async Task OnProfileImageChanged(MultipartFormDataContent multipartFormDataContent)
        {
            await OnProfileImageChange(multipartFormDataContent);
            await OnInitializedAsync();
            this.StateHasChanged();
        }

        public async Task AddNewAddressClicked()
        {
            this.newAddress = new Address();
            newAddress.Location = new Domain.Shared.Shared.Location();

            this.StateHasChanged();

        }

        public async Task OnAddressChange(Address address)
        {
            await UpdateAddress(address);
            this.StateHasChanged();


        }

        public async Task OnNewAddressAdded(Address address)
        {
            await AddNewAddress(address);
            await OnInitializedAsync();
        }

        public async Task OnDeleteAddress(Address address)
        {
            await DeleteAddress(address);
            await OnInitializedAsync();

        }

    }
}
