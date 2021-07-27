using Infrastructure.Scenes.MainMenuPart.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class DataSliderInput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelName;
        [SerializeField] private TextMeshProUGUI _labelCurrentValue;
        [SerializeField] private Slider _slider;

        public void Init(DataSlider dataSlider, object currentValue, UnityAction<object> callback)
        {
            InitText(dataSlider, currentValue);
            InitSlider(dataSlider, currentValue);
            
            _slider.onValueChanged.AddListener(v => 
            {
                callback.Invoke(v);
                _labelCurrentValue.text = ConvertToString(v);
            });
        }

        private void InitText(DataSlider dataSlider, object currentValue)
        {
            _labelCurrentValue.text = ConvertToString(currentValue);
            _labelName.text = dataSlider.NameProperty;
        }

        private static string ConvertToString(object currentValue)
        {
            string valueText = currentValue.ToString();
            if (valueText.Length >= 4)
                valueText = valueText.Substring(0, 4);
            return valueText;
        }

        private void InitSlider(DataSlider dataSlider, object currentValue)
        {
            _slider.minValue = dataSlider.Min;
            _slider.maxValue = dataSlider.Max;
            _slider.value = currentValue is float ? (float) currentValue : (int) currentValue;
            _slider.wholeNumbers = dataSlider.IsWholeNumber;
        }
    }
}