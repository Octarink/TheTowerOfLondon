using System;
using System.Collections.Generic;

namespace Profile
{
    [Serializable]
    public class ProfileData
    {
        public string Name;
        
        public List<bool> PassedLevels = new List<bool>();
    }
}