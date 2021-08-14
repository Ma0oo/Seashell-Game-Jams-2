using System;
using System.Collections;
using Infrastructure.ScenesServices.MainMenuPart.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.ScenesServices.MainMenuPart.Mono
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
            KeyCode keyName = KeyCode.Delete;
            while (keyName == KeyCode.Delete)
            {
                foreach(KeyCode KCode in Enum.GetValues(typeof(KeyCode)))
                    if (Input.GetKeyDown(KCode))
                        keyName = KCode;
                yield return null;
            }

            _newCode = keyName;
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