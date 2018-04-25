using Xamarin.Forms;
using XamarinFormsIOC.Attributes;

namespace XamarinFormsIOC.Views
{
    [ModelToPageDependency(typeof(TestPage1))]
    public partial class TestPage1 : ContentPage
    {
        public TestPage1()
        {
            InitializeComponent();
        }
    }
}