using System;
using Profile;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private EnterView _enterView;
        [SerializeField] private LevelView _levelView;

        private ProfileService _profileService;
        
        [Inject]
        private void Construct(ProfileService profileService)
        {
            _profileService = profileService;
        }
        
        private void Start()
        {
            if (_profileService.CurrentProfile == null)
            {
                _enterView.Activate(true);
                _levelView.Activate(false);
            }
            else
            {
                _levelView.Activate(true);
                _enterView.Activate(false);
            }
        }
    }
}