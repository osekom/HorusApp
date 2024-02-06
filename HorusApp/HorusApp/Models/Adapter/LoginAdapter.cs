using System;
using HorusApp.Models.API;
using HorusApp.Resources;
using Prism.Mvvm;

namespace HorusApp.Models.Adapter
{
	public class LoginAdapter : UserAPI
	{
		//Solicitud
		public string Password { get; set; }

		//Visuales
		public bool isPass { get; set; }
		public string ImageButton
		{
			get
			{
				return (!isPass) ? ResourceApp.eye_none_b : ResourceApp.eye_b;
			}
			set
			{
				ImageButton = value;
			}
		}

	}
}

