using System.Collections.Generic;
using UnityEngine;

namespace Tor
{
    [CreateAssetMenu(fileName = "TorConfig", menuName = "Configs/TorConfig")]
    public class TorConfig : ScriptableObject
    {
        [SerializeField] private List<TorView> _tors;
        
        public List<TorView> Tors => _tors;
        
        public TorView GetByNumber(int number)
        {
            if (_tors.Count > number)
            {
                return _tors[number];
            }

            return null;
        }
    }
}