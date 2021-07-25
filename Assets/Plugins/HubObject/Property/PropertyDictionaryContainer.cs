using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Plugins.HubObject.Property
{
    [Serializable]
    public abstract class PropertyDictionaryContainer<TMain> : MonoBehaviour where TMain : DataProperty
    {
        [SerializeField] private GameObject _objectForData;
        protected Dictionary<Type, TMain> _propDict = new Dictionary<Type, TMain>();

        private void Awake() => MoveToDictFromObject();

        public void Add<T>(T instance) where T : TMain
        {
            if (Has<T>()) throw new Exception("This data allready exist");
            else Set<T>(instance);
        }

        public void Set<T>(T instance) where T :  TMain
        {
            if (Has<T>())
            {
                Destroy(_propDict[typeof(T)]);
                var newComponent = _objectForData.AddComponent<T>();
                _propDict[typeof(T)] = newComponent;
                CloneComponent(instance, newComponent);
            }
            else
            {
                var newComponent = _objectForData.AddComponent<T>();
                _propDict.Add(typeof(T), newComponent);
                CloneComponent(instance, newComponent);
            }
        }

        public bool TryGet<T>(out T result) where T : TMain
        {
            result = GetOrNull<T>();
            return _propDict.ContainsKey(typeof(T));
        }

        public void Remove<T>() where T : TMain
        {
            if (TryGet<T>(out T result))
            {
                Destroy(result);
                _propDict.Remove(typeof(T));
            }
        }
    
        public T GetOrNull<T>() where T : TMain
        {
            if (Has<T>()) return _propDict[typeof(T)] as T;
            else return null;
        }

        public bool Has<T>() where T : TMain => _propDict.ContainsKey(typeof(T));
        
        public List<T> GetAll<T>() where T : TMain
        {
            List<T> result = new List<T>();
            foreach (var data in _propDict.Values) result.Add(data as T);
            return result;
        }

        public void ClearAll()
        {
            foreach (var data in _propDict.Values) Destroy(data);
            _propDict = new Dictionary<Type, TMain>();
        }

        #region utility

        private void MoveToDictFromObject()
        {
            foreach (var data in _objectForData.GetComponents<TMain>()) _propDict.Add(data.GetType(), data);
        }
        
        public void CloneComponent<T>(T componentForClone, T componentToClone) where T : Component
        {
            foreach (var prop in componentForClone.GetType().GetProperties(BindingFlags.Default))
                prop.SetValue(componentToClone, prop.GetValue(componentForClone), BindingFlags.Default, null, null, null);
            foreach (var field in componentForClone.GetType().GetFields(BindingFlags.Default))
                field.SetValue(componentToClone, field.GetValue(componentForClone), BindingFlags.Default, null, null);
        }

        #endregion
    }
}