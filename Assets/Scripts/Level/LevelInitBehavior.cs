using Tor;
using Tower;
using Zenject;

namespace Level
{
    public class LevelInitBehavior : IInitializable
    {
        private LevelSettings _levelSettings;
        private DiContainer _diContainer;
        private TowerPrefabConfig _towerPrefabConfig;
        private TowerLevelsConfig _towerLevelsConfig;
        private TorConfig _torConfig;
        
        public LevelInitBehavior(LevelSettings levelSettings, DiContainer diContainer, TowerPrefabConfig towerPrefabConfig, 
            TowerLevelsConfig towerLevelsConfig, TorConfig torConfig)
        {
            _levelSettings = levelSettings;
            _diContainer = diContainer;
            _towerPrefabConfig = towerPrefabConfig;
            _towerLevelsConfig = towerLevelsConfig;
            _torConfig = torConfig;
        }
        
        public void Initialize()
        {
            BuildLevel();
        }

        private void BuildLevel()
        {
            var tower = _diContainer.InstantiatePrefabForComponent<TowerView>(_towerPrefabConfig.TowerView);
            var levelData = _towerLevelsConfig.GetByNumber(_levelSettings.SelectedLevel);
            if (levelData == null)
            {
                return;
            }
            
            foreach (var i in levelData.Left)
            {
                var prefab = _torConfig.GetByNumber(i);
                var tor = _diContainer.InstantiatePrefabForComponent<TorView>(prefab);
                tor.SetNumber(i);
                tower.PushLeft(tor);
            }
            
            foreach (var i in levelData.Center)
            {
                var prefab = _torConfig.GetByNumber(i);
                var tor = _diContainer.InstantiatePrefabForComponent<TorView>(prefab);
                tor.SetNumber(i);
                tower.PushCenter(tor);
            }
            
            foreach (var i in levelData.Right)
            {
                var prefab = _torConfig.GetByNumber(i);
                var tor = _diContainer.InstantiatePrefabForComponent<TorView>(prefab);
                tor.SetNumber(i);
                tower.PushRight(tor);
            }

            var torCount = levelData.Left.Count + levelData.Center.Count +
                        levelData.Right.Count;
            tower.Initialize(torCount, levelData.Steps);
        }
    }
}