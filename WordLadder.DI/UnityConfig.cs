using System;
using Unity;
using WordLadder.Data;
using WordLadder.Service;

namespace WordLadder.DI
{
    public class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IFileHandler, FileHandler>();
            container.RegisterType<IWordEngine<IWord>, WordEngine>();

            InjectFactory.SetContainer(container);
        }
    }
}
