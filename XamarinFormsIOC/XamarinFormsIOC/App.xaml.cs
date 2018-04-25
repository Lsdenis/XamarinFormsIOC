using Autofac;
using Xamarin.Forms;
using XamarinFormsIOC.Classes;
using XamarinFormsIOC.Enums;
using XamarinFormsIOC.ViewModels;

namespace XamarinFormsIOC
{
    public partial class App : Application
    {
        public App(IComponentContext container)
        {
            NavigationHelper = container.Resolve<NavigationHelper>();

            InitializeComponent();

            NavigationHelper.NavigateAsync<Test1>(PageNavigationType.MainPage);
        }

        public NavigationHelper NavigationHelper { get; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}