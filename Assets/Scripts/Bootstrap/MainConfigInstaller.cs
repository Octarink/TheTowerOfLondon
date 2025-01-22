using Tor;
using Tower;
using UnityEngine;
using Utils;
using Zenject;

namespace Bootstrap
{
    public class MainConfigInstaller : MonoInstaller
    {
        [SerializeField] private TowerLevelsConfig _towerLevelsConfig;
        [SerializeField] private TorConfig _torConfig;
        [SerializeField] private TowerPrefabConfig _towerPrefabConfig;
        
        public override void InstallBindings()
        {
            Container.InstallConfig(_towerLevelsConfig);
            Container.InstallConfig(_torConfig);
            Container.InstallConfig(_towerPrefabConfig);
        }
    }
}