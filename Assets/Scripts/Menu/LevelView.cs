using System;
using Level;
using Profile;
using Tower;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private LevelButtonView _prefab;
        [SerializeField] private Transform _layout;
        [SerializeField] private Button _saveButton;
        
        private Subject<int> _levelSelected = new();
        private TowerLevelsConfig _towerLevelsConfig;
        private LevelSettings _levelSettings;
        private ProfileService _profileService;
        
        [Inject]
        private void Construct(TowerLevelsConfig towerLevelsConfig, LevelSettings levelSettings, ProfileService profileService)
        {
            _towerLevelsConfig = towerLevelsConfig;
            _levelSettings = levelSettings;
            _profileService = profileService;
        }

        private void Start()
        {
            _saveButton.onClick.AddListener(SaveProfile);
        }

        public void Activate(bool active)
        {
            if (active)
            {
                BuildUI();
                _root.SetActive(active);
                _levelSelected.Subscribe(OnLevelSelected);
            }
            else
            {
                _root.SetActive(active);
            }
        }

        private void BuildUI()
        {
            int levelCount = _towerLevelsConfig.Data.Count;
            for (int i = 0; i < levelCount; i++)
            {
                var levelButton = Instantiate(_prefab, _layout);
                var isPassed = _profileService.CurrentProfile.PassedLevels[i];
                levelButton.Initialize(i,_levelSelected,isPassed);
            }
        }

        private void OnLevelSelected(int number)
        {
            _levelSettings.SetNextLevel(number);
            SceneManager.LoadScene("GameScene");
        }

        private void SaveProfile()
        {
            _profileService.SaveProfile();
        }
    }
}