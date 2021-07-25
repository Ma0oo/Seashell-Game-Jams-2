using System;
using System.Collections.Generic;

namespace Plugins.HubObject.GlobalSystem
{
    public class EventChanel
    {
        private static EventChanel _instance;
        public static EventChanel MainChanel => _instance ?? (_instance = new EventChanel());
        
        private Dictionary<Type, Dictionary<string, object>> _singalsContainer;

        public EventChanel()
        {
            _singalsContainer = new Dictionary<Type, Dictionary<string, object>>();
        }
        
        public void RegisterSignal<T>(string id = "") where  T : class
        {
            if(_singalsContainer.ContainsKey(typeof(T)))
                if(_singalsContainer[typeof(T)].ContainsKey(id))
                    throw new Exception($"Container alredy has T ({typeof(T)}) and id ({id})");

            if (_singalsContainer.ContainsKey(typeof(T)))
            {
                _singalsContainer[typeof(T)].Add(id, new ReactOnSignal<T>());
            }
            else
            {
                _singalsContainer.Add(typeof(T), new Dictionary<string, object>());
                _singalsContainer[typeof(T)].Add(id, new ReactOnSignal<T>());
            }
            
        }

        public void UnregisterSignal<T>(string id = "") where  T : class
        {
            if (_singalsContainer.ContainsKey(typeof(T)) && _singalsContainer[typeof(T)].ContainsKey(id)) 
                _singalsContainer[typeof(T)].Remove(id);
            if (_singalsContainer.ContainsKey(typeof(T)) && _singalsContainer[typeof(T)].Count == 0)
                _singalsContainer.Remove(typeof(T));
        }

        public void AddListen<T>(Action<T> method, string id = "", bool throwException = false) where  T : class
        {
            if(_singalsContainer.ContainsKey(typeof(T)))
                if(_singalsContainer[typeof(T)].ContainsKey(id))
                    ((ReactOnSignal<T>) _singalsContainer[typeof(T)][id]).AddListen(method);

            if (throwException)
            {
                if (!_singalsContainer.ContainsKey(typeof(T)))
                    throw new Exception($"No type({typeof(T)}) for add listen");
                if (!_singalsContainer[typeof(T)].ContainsKey(id))
                    throw new Exception($"No id({id}\\{typeof(T)}) for add listen");
            }
        }

        public void RemoveListen<T>(Action<T> method, string id = "", bool throwException = false) where  T : class
        {
            if(_singalsContainer.ContainsKey(typeof(T)))
                if(_singalsContainer[typeof(T)].ContainsKey(id))
                    ((ReactOnSignal<T>) _singalsContainer[typeof(T)][id]).RemoveListen(method);

            if (throwException)
            {
                if (!_singalsContainer.ContainsKey(typeof(T)))
                    throw new Exception($"No type({typeof(T)}) for remove listen");
                if (!_singalsContainer[typeof(T)].ContainsKey(id))
                    throw new Exception($"No id({id}\\{typeof(T)}) for remove listen");
            }
        }

        public void Fire<T>(T payLoad, string id = "") where  T : class
        {
            if(!_singalsContainer.ContainsKey(typeof(T)))
                throw  new Exception($"No type({typeof(T)}) for fire");
            if(!_singalsContainer[typeof(T)].ContainsKey(id))
                throw  new Exception($"No id({id}\\{typeof(T)}) for Fire");
            
            ((ReactOnSignal<T>) _singalsContainer[typeof(T)][id]).Awake(payLoad);
        }

        public bool TryFire<T>(T payLoad, string id = "") where  T : class
        {
            if (_singalsContainer.ContainsKey(typeof(T)))
            {
                if (_singalsContainer[typeof(T)].ContainsKey(id))
                {
                    ((ReactOnSignal<T>) _singalsContainer[typeof(T)][id]).Awake(payLoad);
                    return true;
                }
            }

            return false;
        }
    }

    internal class ReactOnSignal<T>  where  T : class
    {
        private event Action<T> Newsletter;

        public void Awake(T payLoad) => Newsletter?.Invoke(payLoad);

        public void AddListen(Action<T> callback) => Newsletter += callback;
        
        public void RemoveListen(Action<T> callback) => Newsletter -= callback;
    }
}