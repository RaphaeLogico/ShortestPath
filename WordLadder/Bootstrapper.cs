using Unity;
using WordLadder.DI;

namespace WordLadder.App
{
    public static class Bootstrapper
    {
        internal static IUnityContainer Container { get; set; }


        public static void Initialize()
        {
            Container = BuildUnityContainer();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            UnityConfig.Register(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            InjectFactory.SetContainer(container.CreateChildContainer());

            RegisterTypes(container);

            return container;
        }
    }
}
