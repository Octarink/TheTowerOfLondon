using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Profile
{
    public class ProfileService
    {

        public ProfileData CurrentProfile => _currentProfile;
        
        private ProfileData _currentProfile;
        private const string KEY = "PlayerProfilesData";

        public void SetLevelPassed(int number)
        {
            _currentProfile.PassedLevels[number] = true;
        }

        public List<bool> GetLevelPassed()
        {
            return _currentProfile.PassedLevels;
        }
        
        private Profiles LoadData()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var data = PlayerPrefs.GetString(KEY);
                var profiles = JsonConvert.DeserializeObject<Profiles>(data);
                return profiles;
            }
            else
            {
                return null;
            }
        }

        public void SaveProfile()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var data = PlayerPrefs.GetString(KEY);
                var profiles = JsonConvert.DeserializeObject<Profiles>(data);
                var profile = profiles.ProfileList.FirstOrDefault(x => x.Name == _currentProfile.Name);
                if (profile != null)
                {
                    profile.PassedLevels = _currentProfile.PassedLevels;
                }
                else
                {
                    profiles.ProfileList.Add(_currentProfile);
                }

                data = JsonConvert.SerializeObject(profiles);
                PlayerPrefs.SetString(KEY, data);
            }
            else
            {
                var profiles = new Profiles();
                profiles.ProfileList.Add(_currentProfile);
                var data = JsonConvert.SerializeObject(profiles);
                PlayerPrefs.SetString(KEY, data);
            }
        }

        public ProfileData FindProfile(string name)
        {
            var profiles = LoadData();
            if (profiles != null)
            {
                var profile = profiles.ProfileList.FirstOrDefault(x => x.Name == name);
                return profile;
            }
            else
            {
                return null;
            }
        }

        public void SetProfile(ProfileData profile)
        {
            _currentProfile = profile;
        }
    }
}