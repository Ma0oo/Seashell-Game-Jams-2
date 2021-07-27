using System;
using System.Collections;
using Infrastructure.Scenes.MainMenuPart.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class DataKeyboardInput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelField;
        [SerializeField] private TextMeshProUGUI _labelValue;
        [SerializeField] private Button _buttonForChange;

        private KeyCode _newCode;
        private Type _type;

        public void Init(DataKeyboar dataKeyboar, object value, UnityAction<object> actionSetValue)
        {
            InitText(dataKeyboar, value);
            _buttonForChange.onClick.AddListener(() => StartCoroutine(ReadKeyCode(actionSetValue)));
        }

        private IEnumerator ReadKeyCode(UnityAction<object> setKode)
        {
            string keyName = " ";
            while (keyName == " ")
            {
                if (Input.GetMouseButton(0))
                    yield break;

                char firstSymbol = Input.inputString.Length > 0 ? Input.inputString.ToUpper()[0] : ' ';
                
                if (firstSymbol >= 'A' && firstSymbol <= 'Z' && firstSymbol != ' ')
                    keyName = firstSymbol.ToString();
                else if (firstSymbol >= '0' && firstSymbol <= '9' && firstSymbol != ' ')
                    keyName = "Alpha" + firstSymbol;
                else if(firstSymbol != ' ')
                    throw new ArgumentException("Don't know name for this key");
                yield return null;
            }
            _newCode = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            setKode?.Invoke(_newCode);    
            _labelValue.text = _newCode.ToString();
        }
        
        private void InitText(DataKeyboar dataKeyboar, object value)
        {
            _labelField.text = dataKeyboar.NameProperty;
            _labelValue.text = value.ToString();
        }
    }
}