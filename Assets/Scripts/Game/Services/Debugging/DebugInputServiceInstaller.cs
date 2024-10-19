using UnityEngine;
using Zenject;

namespace Game.Services.Debugging
{
    public class DebugInputServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<DebugInputService>().AsSingle().NonLazy();
            Debug.Log($"DebugInputServiceInstaller installed to Project Context");
#endif
        }
    }
}
