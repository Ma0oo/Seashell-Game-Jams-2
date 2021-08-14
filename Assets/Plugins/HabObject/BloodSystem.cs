using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugins.HabObject
{
    public class BloodSystem
    {
        private Dictionary<Type, object> _dictionaryEvent = new Dictionary<Type, object>();

        public void Fire<TSignal>(TSignal instanceSignal) where TSignal : class
        {
            if(instanceSignal==null)
                throw new Exception("Null reference");
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().Awake(instanceSignal);
        }
        
        public void Track<TSignal>(Action<TSignal> tracker) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().AddListen(tracker);
        }

        public void Untrack<TSignal>(Action<TSignal> tracker) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().RemoveListen(tracker);
        }

        public async void Clear()
        {
            Task.Run(() =>
            {
                foreach (var pair in _dictionaryEvent) ((IClearEvent) pair.Value).Clear();
            });
        }

        private Publisher<TSignal> GetPublisherByT<TSignal>() where TSignal : class 
            => (_dictionaryEvent[typeof(TSignal)] as Publisher<TSignal>);

        private bool CheckDictionaryAtHasKeyT<T>() where T : class => _dictionaryEvent.ContainsKey(typeof(T));
        
        private void CreatePublisher<T>() where T : class => _dictionaryEvent.Add(typeof(T), new Publisher<T>());

        private void CheckAndFixIntegrityDictionary<T>() where T : class
        {
            if(!CheckDictionaryAtHasKeyT<T>())
                CreatePublisher<T>();
        }
        
        private class Publisher<T> : IClearEvent where T : class
        {
            private event Action<T> _action;

            public void AddListen(Action<T> action) => _action += action;

            public void RemoveListen(Action<T> action) => _action -= action;

            public void Awake(T payLoad) => _action?.Invoke(payLoad);

            public void Clear() => _action = (Action<T>)Delegate.RemoveAll(_action, _action);
        }
        
        private interface IClearEvent
        {
            void Clear();
        }
    }
}