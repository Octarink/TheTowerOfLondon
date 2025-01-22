using System;
using Level;
using Profile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;


namespace GameUI
{
    public class WinView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private Button _nextButton;

        private SignalBus _signalBus;
        private ProfileService _profileService;
        private LevelSettings _levelSettings;
        
        [Inject]
        private void Construct(SignalBus signalBus, ProfileService profileService, LevelSettings levelSettings)
        {
            _signalBus = signalBus;
            _profileService = profileService;
            _levelSettings = levelSettings;
        }
        
        private void Start()
        {
            _root.SetActive(false);
            _signalBus.Subscribe<LevelSignals.Win>(Activate);
            _nextButton.onClick.AddListener(OnNext);
        }

        private void Activate()
        {
            _root.SetActive(true);
        }

        private void OnNext()
        {
            _profileService.CurrentProfile.PassedLevels[_levelSettings.SelectedLevel] = true;
            SceneManager.LoadScene("MenuScene");
            
        }
    }
}