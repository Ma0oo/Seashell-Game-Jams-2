using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Plugins.HabObject
{
    public class ComponentShell : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjectWithComponents;
        [SerializeField] private GameObject _objectForNewComponent;

        public T Get<T>() where T : Component
        {
            T result = null;
            foreach (var objectGame in _gameObjectWithComponents)
                if (objectGame.TryGetComponent<T>(out result))
                    return result;
            if (_objectForNewComponent.TryGetComponent<T>(out result))
                return result;
            return result;
        }

        public List<T> GetAll<T>()
        {
            List<T> results = new List<T>();
            foreach (var componentObject in _gameObjectWithComponents)
                foreach (var resultT in componentObject.GetComponents<T>())
                    results.Add(resultT);
            foreach (var resultT in _objectForNewComponent.GetComponents<T>()) results.Add(resultT);
            return results;
        }

        public Component Get(Type type)
        {
            Component result = null;
            foreach (var objectGame in _gameObjectWithComponents)
                if (objectGame.TryGetComponent(type, out result))
                    return result;
            if (_objectForNewComponent.TryGetComponent(type, out result))
                return result;
            return result;
        }

        public bool HasInstance<T>(T target) where T : Component
        {
            foreach (var comp in GetComponentsInChildren<T>())
                if (comp == target)
                    return true;
            return false;
        }
        
        public bool TryGet<T>(out T result) where T : Component
        {
            result = Get<T>();
            return result != null;
        }

        public T Add<T>() where T : Component => _objectForNewComponent.AddComponent<T>();

        public T CloneToMe<T>(T componentForClone) where T : Component
        {
            T newComponent = Add<T>();
            foreach (var prop in componentForClone.GetType().GetProperties(BindingFlags.Default))
                prop.SetValue(newComponent, prop.GetValue(componentForClone), BindingFlags.Default, null, null, null);
            foreach (var field in componentForClone.GetType().GetFields(BindingFlags.Default))
                field.SetValue(newComponent, field.GetValue(componentForClone), BindingFlags.Default, null, null);
            return newComponent;
        }
    }
}