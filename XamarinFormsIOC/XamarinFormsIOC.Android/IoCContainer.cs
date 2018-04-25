using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using XamarinFormsIOC.Attributes;

namespace XamarinFormsIOC.Droid
{
    public abstract class IoCContainer
    {
        public static IContainer Initialize()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            foreach (var assembly in GetAssemblies())
            {
                var selfTypes = assembly.GetTypes()
                    .Where(type => !type.IsAbstract &&
                                   type.GetCustomAttributes()
                                       .Any(type1 => type1.GetType() == typeof(SelfInjectionAttribute)))
                    .ToList();

                foreach (var type in selfTypes)
                {
                    containerBuilder.RegisterType(type).SingleInstance();
                }

                var interfaceTypes = assembly.GetTypes()
                    .Where(type =>
                        type.GetCustomAttributes().Any(type1 => type1.GetType() == typeof(InterfaceInjectionAttribute)))
                    .ToList();

                foreach (var type in interfaceTypes)
                {
                    containerBuilder.RegisterType(type).As(type.GetInterfaces());
                }
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return Assembly.GetExecutingAssembly();
            yield return typeof(App).Assembly;
        }
    }
}