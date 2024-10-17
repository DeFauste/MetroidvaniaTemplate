using Zenject;

namespace Game.Services.Debugging
{
    public class DebugInputServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<DebugInputService>().AsSingle().NonLazy();
#endif
        }
    }
}
