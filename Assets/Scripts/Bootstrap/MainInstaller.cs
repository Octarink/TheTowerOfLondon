using Level;
using Profile;
using Tower;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<LevelSettings>().AsSingle().NonLazy();
            Container.Bind<ProfileService>().AsSingle().NonLazy();

            Container.DeclareSignal<LevelSignals.Win>();
            Container.DeclareSignal<LevelSignals.Lose>();
        }
    }
}