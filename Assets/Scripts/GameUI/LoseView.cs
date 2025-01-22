using Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class LoseView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _restartButton;

        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            _root.SetActive(false);
            _signalBus.Subscribe<LevelSignals.Lose>(Activate);
            _restartButton.onClick.AddListener(OnRestart);
            _nextButton.onClick.AddListener(OnNext);
        }

        private void Activate()
        {
            _root.SetActive(true);
        }

        private void OnNext()
        {
            SceneManager.LoadScene("MenuScene");
        }
        
        private void OnRestart()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}