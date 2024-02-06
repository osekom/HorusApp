using System.Collections.Generic;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using HorusApp.Abstractions;
using HorusApp.Models.API;
using HorusApp.Resources;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;

namespace HorusApp.ViewModels.Popups
{
    public class ChallengePagePopupViewModel : ViewModelBase
    {
        #region Vars
        private static string TAG = nameof(ChallengePagePopupViewModel);
        #endregion

        #region Vars Commands
        public DelegateCommand CancelCommand { get; set; }
        #endregion

        #region Properties

        public string title { get; set; }
        public string description { get; set; }
        public double PercetTask { get; set; }

        #endregion

        public ChallengePagePopupViewModel(INavigationService navigationService,
                                     IUserDialogs userDialogsService,
                                     IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            CancelCommand = new DelegateCommand(CancelCommandExecute);
        }

        #region Methods Commands

        private async void CancelCommandExecute()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion

        #region Methods

        private void PopulateView(ChallengesAPI model)
        {
            title = model.title;
            description = model.description;
            PercetTask = model.PercentChallenge;
        }

        #endregion

        #region Navigation Methods
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Challenge"))
            {
                PopulateView(parameters.GetValue<ChallengesAPI>("Challenge"));
                
            }
        }
        #endregion
    }
}
