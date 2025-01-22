using System;
using System.Collections.Generic;

namespace Tower
{
    [Serializable]
    public class TowerData
    {
        public List<int> Left = new ();
        public List<int> Center = new ();
        public List<int> Right = new();

        public int Steps;
    }
}