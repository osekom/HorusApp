using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HorusApp.Settings;
using HorusApp.LocalData;

namespace HorusApp.Services
{
    public class AuthenticationToken
    {
        public string Access_token { get; set; }

        public string Token_type { get; set; }

        int _expires_in;
        public int Expires_in
        {
            get
            {
                return _expires_in;
            }

            set
            {
                _expires_in = value;
            }
        }

        public DateTime? ExpireTime { get; set; }

        public bool IsExpired
        {
            get
            {
                if (ExpireTime != null && DateTime.Now < ExpireTime)
                {
                    return false;
                }

                return true;
            }
        }
    }

    public static class TokenManager
    {
        public static string TAG = nameof(TokenManager);
        public static AuthenticationToken AuthenticationToken;
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static async Task<string> GetToken()
        {
            return Profile.Instance.AuthorizationToken;
        }
    }
}
