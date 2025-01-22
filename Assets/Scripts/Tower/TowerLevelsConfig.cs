using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerLevelsConfig", menuName = "Configs/TowerLevelsConfig")]
    public class TowerLevelsConfig : ScriptableObject
    {
        [SerializeField] private List<TowerData> _data = new ();

        public List<TowerData> Data => _data;

        public TowerData GetByNumber(int number)
        {
            if (_data.Count > number)
            {
                return _data[number];
            }

            return null;
        }
    }
}