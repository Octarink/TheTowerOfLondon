using TMPro;
using Tower;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class StepsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private TowerModel _towerModel;
        
        [Inject]
        private void Construct(TowerModel towerModel)
        {
            _towerModel = towerModel;
        }
        
        private void Start()
        {
            _text.text = _towerModel.MaxSteps.ToString();
            _towerModel.CurrentSteps.Subscribe(UpdateSteps);
        }

        private void UpdateSteps(int step)
        {
            var leftStep = _towerModel.MaxSteps - step;
            leftStep = leftStep < 0 ? 0 : leftStep;
            _text.text = leftStep.ToString();
        }
    }
}