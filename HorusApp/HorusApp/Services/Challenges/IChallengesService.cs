using System.Collections.Generic;
using System.Threading.Tasks;
using HorusApp.Models.Adapter;
using HorusApp.Models.API;
using Refit;

namespace HorusApp.Services.Challenges
{
    public interface IChallengesService
    {
        [Get("/Challenges")]
        Task<List<ChallengesAPI>> Challenges([Header("Authorization")] string Token);
    }
}
