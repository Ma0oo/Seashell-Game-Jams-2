using System;
using System.Collections.Generic;
using System.Reflection;
using Infrastructure.Data;
using Infrastructure.GameStateMachines.States;
using Infrastructure.Scenes.MainMenuPart.Attribute;
using Infrastructure.Services;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class DataViewMono : MonoBehaviour
    {
        [SerializeField] private Transform _parentOfInputs;
        [SerializeField] private DataSliderInput _sliderInputTemplate;
        [SerializeField] private DataButtonInput _buttonInputTemplate;
        [SerializeField] private DataKeyboardInput _keyboardInput;
        
        [DI] private DataProvider _dataProvider;
        [DI] private ProfileProvider _profileEditor;
        [DI(MainMenu.EventChanelId)] private EventChanel _eventChanel;

        private IData _currentData;
        private List<GameObject> _inputObjects = new List<GameObject>();
        private Dictionary<FieldInfo, DataAttribute> _currentListNameAndAttribute;


        public void SaveCurrentData()
        {
            if(_currentData!=null)
                _dataProvider.Save(_currentData);
        }
        
        public void Spawn(int number)
        {
            RemoveOldField();
            DataForView dataEnum = (DataForView)Enum.Parse(typeof(DataForView), number.ToString());
            switch (dataEnum)
            {
                case DataForView.Sound:
                    Generate<DataSound>();
                    break;
                case DataForView.Graphics:
                    Generate<GraphicsData>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(DataForView), dataEnum, null);
            }
        }

        private void RemoveOldField()
        {
            for (int i = 0; i < _inputObjects.Count; i++) Destroy(_inputObjects[i].gameObject);
            _inputObjects = new List<GameObject>();
        }
        
        private void Generate<T>() where  T : class, IData, new()
        {
            var data = _dataProvider.Get<T>();
            _currentData = data;
            _currentListNameAndAttribute = FieldAndAttribute(data);
            SpawnInputFieldDataSound(data, _currentListNameAndAttribute);
        }
        
        public enum DataForView
        {
            Sound,
            Graphics
        }

        #region SpawnInputFieldsByData

        private void SpawnInputFieldDataSound(IData data, Dictionary<FieldInfo, DataAttribute> dictionary)
        {
            foreach (var pair in dictionary)
            {
                var currentValue = pair.Key.GetValue(data);
                if (pair.Value is DataSlider) SpawnSliderInput(pair, currentValue, data);
                else if (pair.Value is DataButton) SpawnButtonInput(pair, currentValue, data);
                else if (pair.Value is DataKeyboar) SpawnKeyBoardInut(pair, currentValue, data);
                else throw new Exception($"Unknowns DataAttribute - {pair.Value.GetType()}");
            }
        }

        #endregion

        #region Other

        private static Dictionary<FieldInfo, DataAttribute> FieldAndAttribute(IData data)
        {
            var fields = data.GetType().GetFields();
            var nameAndAttribute = new Dictionary<FieldInfo, DataAttribute>();
            foreach (var prop in fields)
                if (prop.GetCustomAttribute<DataAttribute>() != null)
                    nameAndAttribute.Add(prop, prop.GetCustomAttribute<DataAttribute>());
            return nameAndAttribute;
        }

        #endregion
        
        #region SpawnInputFieldMethod
        
        private void SpawnKeyBoardInut(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataButton = pair.Value as DataKeyboar;

            DataKeyboardInput dataKeyboardInput = Instantiate(_keyboardInput, _parentOfInputs);
            dataKeyboardInput.Init(
                dataButton,
                currentValue,
                v => pair.Key.SetValue(data, v));

            _inputObjects.Add(dataKeyboardInput.gameObject);
        }

        private void SpawnButtonInput(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataButton = pair.Value as DataButton;

            DataButtonInput dataButtonInput = Instantiate(_buttonInputTemplate, _parentOfInputs);
            dataButtonInput.Init(
                dataButton,
                currentValue,
                v => pair.Key.SetValue(data, v));

            _inputObjects.Add(dataButtonInput.gameObject);
        }

        private void SpawnSliderInput(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataSlider = pair.Value as DataSlider;

            DataSliderInput newInstance = Instantiate(_sliderInputTemplate, _parentOfInputs);
            newInstance.Init(dataSlider,
                currentValue,
                v =>
                {
                    if(pair.Key.GetValue(data) is int)
                        pair.Key.SetValue(data, Convert.ChangeType(v, typeof(int)));
                    else
                        pair.Key.SetValue(data, v);
                });

            _inputObjects.Add(newInstance.gameObject);
        }
        
        #endregion
    }
}