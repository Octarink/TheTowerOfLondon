using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerPrefabConfig", menuName = "Configs/TowerPrefabConfig")]
    public class TowerPrefabConfig : ScriptableObject
    {
        [SerializeField] private TowerView _towerView;

        public TowerView TowerView => _towerView;
    }
}