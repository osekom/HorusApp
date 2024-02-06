using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using HorusApp.Settings;
using HorusApp.LocalData;
using HorusApp.Models.API;
using HorusApp.Services.Session;
using System.Collections.Generic;

namespace HorusApp.Services.Challenges
{
    public class ChallengesService : IChallengesService
    {
        private readonly IChallengesService challengesService;

        public ChallengesService()
        {
            challengesService = RestService.For<IChallengesService>(new HttpClient(new HttpLoggingHandler(TokenManager.GetToken)) { BaseAddress = new Uri(AppConfiguration.Values.BaseUrl) }, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CustomResolver()
                }
            });
        }

        public Task<List<ChallengesAPI>> Challenges(string Token)
        {
            return challengesService.Challenges(Token);
        }

    }
}
