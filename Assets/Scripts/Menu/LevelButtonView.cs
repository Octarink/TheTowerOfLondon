using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _passed;
        [SerializeField] private Color _default;
        [SerializeField] private Image _image;

        private Subject<int> _levelSelected;
        private int _number;
        
        public void Initialize(int number, Subject<int> levelSelected, bool passed)
        {
            _number = number;
            _text.text = (number + 1).ToString();
            _levelSelected = levelSelected;
            _button.onClick.AddListener(OnClick);
            if (passed)
            {
                var colors = _button.colors;
                colors.normalColor = _passed;
                _button.colors = colors;
                //_image.material.color = _passed;
            }
            else
            {
                var colors = _button.colors;
                colors.normalColor = _default;
                _button.colors = colors;
                //_image.material.color = _default;
            }
        }

        private void OnClick()
        {
            _levelSelected?.OnNext(_number);
        }
    }
}