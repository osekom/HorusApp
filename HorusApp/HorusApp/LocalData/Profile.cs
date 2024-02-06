using Plugin.Settings.Abstractions;

namespace HorusApp.LocalData
{
    public class Profile
    {
        /// <summary>
        /// Settings service
        /// </summary>
        private ISettings settingsService;

        /// <summary>
        /// Instance
        /// </summary>
        private static Profile instance;

        /// <summary>
        /// Instance
        /// </summary>
        public static Profile Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new Profile();
                }

                return instance;
            }
        }

        /// <summary>
        /// Initialize the app profile data service
        /// </summary>
        /// <param name="settingsService">Settings Service</param>
        public void Initialize(ISettings settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Authorization Token
        /// </summary>
        public string AuthorizationToken
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(AuthorizationToken)}", default(string));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(AuthorizationToken)}", value);
            }
        }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(Email)}", default(string));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(Email)}", value);
            }
        }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(FirstName)}", default(string));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(FirstName)}", value);
            }
        }

        /// <summary>
        /// Sur Name
        /// </summary>
        public string SurName
        {
            get => settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(SurName)}", default(string));
            set => settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(SurName)}", value);
        }

        /// <summary>
        /// Clear values
        /// </summary>
        public void ClearValues()
        {
            AuthorizationToken = default(string);
            Email = default(string);
            FirstName = default(string);
            SurName = default(string);
        }
    }
}
