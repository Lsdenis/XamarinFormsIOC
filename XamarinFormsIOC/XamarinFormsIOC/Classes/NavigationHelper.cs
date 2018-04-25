using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Xamarin.Forms;
using XamarinFormsIOC.Attributes;
using XamarinFormsIOC.Enums;
using XamarinFormsIOC.Interfaces;
using XamarinFormsIOC.ViewModels;

namespace XamarinFormsIOC.Classes
{
    [SelfInjection]
    public class NavigationHelper
    {
        private readonly ILifetimeScope _lifetimeScope;

        public NavigationHelper(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        private Page MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }

        public async Task NavigateAsync<TViewModel, TModel>(
            TModel data,
            PageNavigationType navigationType = PageNavigationType.Default,
            bool awaitDataBeenLoaded = false)
            where TViewModel : BaseViewModel, IInitViewModel<TModel>
            where TModel : class
        {
            await ExecuteNavigationAsync<TViewModel>(navigationType, awaitDataBeenLoaded,
                viewModel => { viewModel.InitModel(data); });
        }

        private async Task ExecuteNavigationAsync<TViewModel>(
            PageNavigationType navigationType,
            bool awaitDataBeenLoaded,
            Action<TViewModel> initModelAction = null)
            where TViewModel : BaseViewModel
        {
            var viewModel = _lifetimeScope.Resolve<TViewModel>();

            var pageType = (typeof(TViewModel).GetCustomAttributes(typeof(ModelToPageDependencyAttribute), true)
                .FirstOrDefault() as ModelToPageDependencyAttribute)?.Page;

            if (pageType == null)
            {
                throw new NullReferenceException("Page type is not set to ViewModel");
            }

            var page = (Page) Activator.CreateInstance(pageType);
            initModelAction?.Invoke(viewModel);
            if (awaitDataBeenLoaded)
            {
                await viewModel.LoadData();
            }
            else
            {
                viewModel.LoadData();
            }

            page.BindingContext = viewModel;

            await PerformNavigationActionAsync(page, navigationType);
        }

        private async Task PerformNavigationActionAsync(Page page, PageNavigationType navigationType)
        {
            switch (navigationType)
            {
                case PageNavigationType.Modal:
                {
                    if (MainPage == null)
                    {
                        throw new NullReferenceException("Main page is not set");
                    }

                    await MainPage.Navigation.PushModalAsync(page);
                    break;
                }
                case PageNavigationType.Popup:
                {
                    break;
                }
                case PageNavigationType.MainPage:
                {
                    MainPage = page;
                    break;
                }
                default:
                {
                    if (MainPage == null)
                    {
                        throw new NullReferenceException("Main page is not set");
                    }

                    await MainPage.Navigation.PushAsync(new NavigationPage(page));
                    break;
                }
            }
        }

        public async Task NavigateAsync<TViewModel>(
            PageNavigationType navigationType = PageNavigationType.Default,
            bool awaitDataBeenLoaded = false)
            where TViewModel : BaseViewModel
        {
            await ExecuteNavigationAsync<TViewModel>(navigationType, awaitDataBeenLoaded);
        }

        public async Task PopAsync(PageNavigationType navigationType = PageNavigationType.Default)
        {
            switch (navigationType)
            {
                case PageNavigationType.Modal:
                {
                    if (MainPage.Navigation.ModalStack.Count < 1)
                    {
                        break;
                    }

                    await MainPage.Navigation.PopModalAsync();
                    break;
                }
                case PageNavigationType.Popup:
                {
                    break;
                }
                case PageNavigationType.MainPage:
                {
                    if (MainPage.Navigation.NavigationStack.Count < 1)
                    {
                        break;
                    }

                    await MainPage.Navigation.PopToRootAsync();
                    break;
                }
                default:
                {
                    if (MainPage.Navigation.NavigationStack.Count < 1)
                    {
                        break;
                    }

                    await MainPage.Navigation.PopAsync();
                    break;
                }
            }
        }
    }
}