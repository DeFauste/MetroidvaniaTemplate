using Zenject;
using UnityEngine;

namespace Game.Services.Debugging
{
    public class DebugInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<DebugInputService>().AsSingle().NonLazy();
#endif
        }
    }
}
