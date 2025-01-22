using Level;
using Tower;
using Zenject;

namespace Bootstrap
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelInitBehavior>().AsSingle().NonLazy();
            Container.Bind<TowerModel>().AsSingle().NonLazy();
        }
    }
}