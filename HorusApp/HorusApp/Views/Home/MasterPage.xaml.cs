using Xamarin.Forms;

namespace HorusApp.Views.Home
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            App.Master = this;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
