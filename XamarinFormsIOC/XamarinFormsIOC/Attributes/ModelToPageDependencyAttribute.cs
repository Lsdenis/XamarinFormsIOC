using System;
using Xamarin.Forms;

namespace XamarinFormsIOC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelToPageDependencyAttribute : Attribute
    {
        public Type Page { get; }

        public ModelToPageDependencyAttribute(Type page)
        {
            Page = page;
            var baseType = page.BaseType;
            do
            {
                if (baseType == typeof(Page))
                {
                    return;
                }

                baseType = baseType.BaseType;
            } while (baseType != null);

            throw new ArgumentException("Type should be Page");
        }
    }
}