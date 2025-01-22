using System;
using System.Collections.Generic;
using Profile;
using TMPro;
using Tower;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class EnterView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _button;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private GameObject _root;

        private ProfileService _profileService;
        private TowerLevelsConfig _towerLevelsConfig;
        
        [Inject]
        private void Construct(ProfileService profileService, TowerLevelsConfig towerLevelsConfig)
        {
            _profileService = profileService;
            _towerLevelsConfig = towerLevelsConfig;
        }
        
        private void Start()
        {
            _button.onClick.AddListener(OnEnter);
        }

        private void OnEnter()
        {
            if (_inputField.text != String.Empty)
            {
                var nick = _inputField.text;
                var findProfile = _profileService.FindProfile(nick);
                if (findProfile != null)
                {
                    _profileService.SetProfile(findProfile);
                }
                else
                {
                    var profile = new ProfileData();
                    profile.Name = nick;
                    for (int i = 0; i < _towerLevelsConfig.Data.Count; i++)
                    {
                        profile.PassedLevels.Add(false);
                    }
                    _profileService.SetProfile(profile);
                }
                Activate(false);
                _levelView.Activate(true);
            }
        }

        public void Activate(bool active)
        {
            _root.SetActive(active);
        }
    }
}