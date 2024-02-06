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
using HorusApp.Resources;

namespace HorusApp.ViewModels.Session
{
    public class LogInPageViewModel : ViewModelBase
    {
        
        #region Vars
        private static string TAG = nameof(LogInPageViewModel);

        public string EMPTY_EMAIL = "Campo usuario vacio.";
        public string EMPTY_PASS = "Campo contraseña vacio.";
        public string ERROR_TOKEN = "Error al iniciar sesión, comuníquese con soporte.";
        public string WRONG_CREDENTIAL = "Credenciales incorrectas.";
        public string SERVER_ERROR = "Error conectando con el servidor; reintente más tarde.";

        private ISessionService _sessionService;
        #endregion

        #region Vars Commands
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand ShowHidePassCommand { get; set; }
        public DelegateCommand ForgotPassCommand { get; set; }
        public DelegateCommand FacebookCommand { get; set; }
        public DelegateCommand InstagramCommand { get; set; }
        public DelegateCommand RegistrarmeCommand { get; set; }
        #endregion

        #region Properties

        public LoginAdapter LoginAdapterData { get; set; }

        #endregion

        #region Contructor
        public LogInPageViewModel(ISessionService sessionService,
                                  INavigationService navigationService,
                                  IUserDialogs userDialogsService,
                                  IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            _sessionService = sessionService;
            LogInCommand = new DelegateCommand(LogInCommandExecuted);
            ShowHidePassCommand = new DelegateCommand(ShowHidePassCommandExecuted);
            ForgotPassCommand = new DelegateCommand(ForgotPassCommandExecuted);
            FacebookCommand = new DelegateCommand(FacebookCommandExecuted);
            InstagramCommand = new DelegateCommand(InstagramCommandExecuted);
            RegistrarmeCommand = new DelegateCommand(RegistrarmeCommandExecuted);
            LoginAdapterData = new LoginAdapter();
            LoginAdapterData.isPass = true;
        }
        #endregion

        #region Commands Methods


        private void ForgotPassCommandExecuted()
        {
            ShowMessageGeneral("Olvidaste tu contraseña");
        }

        private void FacebookCommandExecuted()
        {
            ShowMessageGeneral("Facebook");
        }

        private void InstagramCommandExecuted()
        {
            ShowMessageGeneral("Instagram");
        }

        private void RegistrarmeCommandExecuted()
        {
            ShowMessageGeneral("Registrarme");
        }

        //Ocultar boton de ver contrasena
        private void ShowHidePassCommandExecuted()
        {
            LoginAdapterData.isPass = !LoginAdapterData.isPass;
        }

        //login
        private async void LogInCommandExecuted()
        {

#if DEBUG
            LoginAdapterData.email = "user3@mail.com";
            LoginAdapterData.Password = "Password3";
#endif

            UserDialogsService.ShowLoading("Verificando...");
            if(string.IsNullOrEmpty(LoginAdapterData.email))
            {
                UserDialogsService.Alert(EMPTY_EMAIL, "Alerta", "Aceptar");
                UserDialogsService.HideLoading();
                return;
            }
            if (string.IsNullOrEmpty(LoginAdapterData.Password))
            {
                UserDialogsService.Alert(EMPTY_PASS, "Alerta", "Aceptar");
                UserDialogsService.HideLoading();
                return;
            }
            var resp = await RunSafeApi<UserAPI>(_sessionService.AuthUser(LoginAdapterData));
            if (resp.Status == TypeReponse.Ok && resp.Response != null)
            {
                //Verica si contiene un Auh Token valido.
                if (string.IsNullOrWhiteSpace(resp.Response.authorizationToken))
                {
                    UserDialogsService.Alert(ERROR_TOKEN, "Alerta", "Aceptar");
                    return;
                }
                SaveSession(resp.Response);
                UserDialogsService.HideLoading();
                await NavigationService.NavigateAsync($"{ResourceApp.ChallengesPage}");
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                UserDialogsService.Alert(WRONG_CREDENTIAL, "Alerta", "Aceptar");
            }
            else
            {
                UserDialogsService.Alert(SERVER_ERROR, "Alerta", "Aceptar");
            }
            UserDialogsService.HideLoading();
        }
        #endregion

        #region Methods

        public void ShowMessageGeneral(string buttonName)
        {
            UserDialogsService.Alert($"{buttonName}", "Alerta", "Aceptar");
        }

        private void SaveSession(UserAPI user)
        {
            Profile.Instance.Email = user.email;
            Profile.Instance.FirstName = user.firstname;
            Profile.Instance.SurName = user.surname;

            AppSettings.Instance.Logged = true;
        }
        #endregion

    }
}
