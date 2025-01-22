using UnityEngine;

namespace Tor
{
    public class TorView : MonoBehaviour
    {
        public int Number => _number;
        
        private int _number;

        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}