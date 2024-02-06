using System.Threading.Tasks;
using HorusApp.Models.Adapter;
using HorusApp.Models.API;
using Refit;

namespace HorusApp.Services.Session
{
    public interface ISessionService
    {
        [Post("/UserSignIn")]
        Task<UserAPI> AuthUser([Body] LoginAdapter model);
    }
}
