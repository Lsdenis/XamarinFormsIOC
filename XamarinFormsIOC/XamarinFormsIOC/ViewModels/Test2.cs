using XamarinFormsIOC.Interfaces;

namespace XamarinFormsIOC.ViewModels
{
    public class Test2 : BaseViewModel
    {
        private readonly ITest2 _test2;

        public Test2(ITest2 test2)
        {
            _test2 = test2;
        }
    }
}