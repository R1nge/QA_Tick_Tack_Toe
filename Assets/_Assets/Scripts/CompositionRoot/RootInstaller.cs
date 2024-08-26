using _Assets.Scripts.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private ConfigProvider configProvider;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(configProvider);
            builder.Register<SceneSerivce>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}