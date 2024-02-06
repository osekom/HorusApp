using System;
using HorusApp.Abstractions;
using HorusApp.LocalData;

namespace HorusApp.Models.API
{
	public class UserAPI : ModelBase
	{
		public string email { get; set; }
		public string firstname { get; set; }
		public string surname { get; set; }
		public string authorizationToken
		{
			get
			{
				return Profile.Instance.AuthorizationToken;
			}
			set
			{
				Profile.Instance.AuthorizationToken = value;
			}
		}
	}
}