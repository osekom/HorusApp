using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using HorusApp.Helpers;
using HorusApp.Settings;
using HorusApp.LocalData;
using HorusApp.Models.API;
using HorusApp.Models.Adapter;

namespace HorusApp.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly ISessionService sessionService;

        public SessionService()
        {
            sessionService = RestService.For<ISessionService>(new HttpClient(new HttpLoggingHandler(TokenManager.GetToken)) { BaseAddress = new Uri(AppConfiguration.Values.BaseUrl) }, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CustomResolver()
                }
            });
        }

        public Task<UserAPI> AuthUser(LoginAdapter model)
        {
            return sessionService.AuthUser(model);
        }

    }
}
