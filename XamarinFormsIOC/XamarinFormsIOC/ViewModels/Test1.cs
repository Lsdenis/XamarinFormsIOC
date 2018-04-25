using System.Threading.Tasks;
using XamarinFormsIOC.Attributes;
using XamarinFormsIOC.Interfaces;

namespace XamarinFormsIOC.ViewModels
{
    [ModelToPageDependency(typeof(Views.MainPage))]
    public class Test1 : BaseViewModel, IInitViewModel<Test1>
    {
        private readonly ITest1 _test1;
        private readonly ITest2 _test2;

        public Test1(ITest1 test1, ITest2 test2)
        {
            _test1 = test1;
            _test2 = test2;
        }

        public async Task InitModel(Test1 data)
        {
        }
    }
}