using System;
using System.Diagnostics;
using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using HorusApp.Abstractions;
using HorusApp.LocalData;
using HorusApp.Services.Session;
using HorusApp.Models.API;
using HorusApp.Models.Adapter;
using Xamarin.Forms;
using HorusApp.Services.Challenges;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HorusApp.Resources;

namespace HorusApp.ViewModels.Home
{
    public class ChallengesPageViewModel : ViewModelBase
    {
        
        #region Vars
        private static string TAG = nameof(ChallengesPageViewModel);

        public string SESION_CADUCADA = "Sesión caducada, debe iniciar sesión nuevamente.";

        private IChallengesService _challengesService;
        #endregion

        #region Vars Commands
        public DelegateCommand<object> OpenChallengeCommand { get; set; }
        #endregion

        #region Properties

        public int CompletesChallenges { get; set; }
        public int CountsChallenges { get; set; }
        public ObservableCollection<ChallengesAPI> challengesAdapter { get; set; }
    
        #endregion

        #region Contructor
        public ChallengesPageViewModel(IChallengesService challengesService,
                                  INavigationService navigationService,
                                  IUserDialogs userDialogsService,
                                  IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            _challengesService = challengesService;
            OpenChallengeCommand = new DelegateCommand<object>(OpenChallengeCommandExecuted);
        }
        #endregion

        #region Commands Methods

        private async void OpenChallengeCommandExecuted(object item)
        {
            if (item is ChallengesAPI)
            {
                NavigationParameters parameter = new NavigationParameters
                {
                    { "Challenge", (ChallengesAPI)item }
                };
                await NavigationService.NavigateAsync(ResourceApp.ChallengePopup, parameter);
            }
        }


        #endregion

        #region Methods
        private async void PopulateView()
        {
            UserDialogsService.ShowLoading("Cargando retos...");
            var responseChallenge = await RunSafeApi<List<ChallengesAPI>>(_challengesService.Challenges(Profile.Instance.AuthorizationToken));
            if (responseChallenge.Status == TypeReponse.Ok && responseChallenge.Response != null)
            {
                challengesAdapter = new ObservableCollection<ChallengesAPI>(responseChallenge.Response);
                CountsChallenges = challengesAdapter.Count;
                CompletesChallenges = challengesAdapter.Count(c => c.currentPoints == c.totalPoints);
            }
            else if (responseChallenge.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await UserDialogsService.AlertAsync(SESION_CADUCADA, "Alerta", "Aceptar");
                AppSettings.Instance.ClearValues();
                Profile.Instance.ClearValues();
                await NavigationService.NavigateAsync(new Uri($"/Navigation/{ResourceApp.LogInPage}", UriKind.Absolute));
            }
            UserDialogsService.HideLoading();
        }
        #endregion


        #region Navigation Params
        #endregion

        #region Methods Life Cycle Page
        public override void OnAppearing()
        {
            try
            {
                PopulateView();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message, TAG);
            }

        }
        #endregion
    }
}
