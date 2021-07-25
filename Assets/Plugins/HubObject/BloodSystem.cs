using System;
using System.Collections.Generic;

namespace Plugins.HubObject
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

        private Publisher<TSignal> GetPublisherByT<TSignal>() where TSignal : class
        {
            return (_dictionaryEvent[typeof(TSignal)] as Publisher<TSignal>);
        }

        private bool CheckDictionaryAtHasKeyT<T>() where T : class => _dictionaryEvent.ContainsKey(typeof(T));
        
        private void CreatePublisher<T>() where T : class => _dictionaryEvent.Add(typeof(T), new Publisher<T>());

        private void CheckAndFixIntegrityDictionary<T>() where T : class
        {
            if(!CheckDictionaryAtHasKeyT<T>())
                CreatePublisher<T>();
        }

        private class Publisher<T> where T : class
        {
            private event Action<T> _actions;

            public void AddListen(Action<T> action) => _actions += action;

            public void RemoveListen(Action<T> action) => _actions -= action;

            public void Awake(T payLoad) => _actions?.Invoke(payLoad);
        }
    }
}