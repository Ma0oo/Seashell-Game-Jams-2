using System;
using System.Collections.Generic;

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
        
        public void Track<TSignal>(Action<TSignal> tracker, Priority priority = Priority.Third) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().AddListen(tracker, priority);
        }

        public void Untrack<TSignal>(Action<TSignal> tracker, Priority priority = Priority.Third) where TSignal : class
        {
            CheckAndFixIntegrityDictionary<TSignal>();
            GetPublisherByT<TSignal>().RemoveListen(tracker, priority);
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

        public enum Priority
        {
            First, Second, Third, Fourth, Fifth 
        }
        
        private class Publisher<T> where T : class
        {
            SignalElement<T>[] _signals = new SignalElement<T>[5];
            
            public Publisher()
            {
                for (int i = 0; i < _signals.Length; i++) _signals[i] = new SignalElement<T>();
            }
            
            public void AddListen(Action<T> action, Priority priority) => _signals[(int) priority].Add(action);

            public void RemoveListen(Action<T> action, Priority priority) => _signals[(int)priority].Remove(action);

            public void Awake(T payLoad)
            {
                foreach (var signal in _signals) signal.Awake(payLoad);
            }
        }
        
        private class SignalElement<T> where T : class
        {
            private event Action<T> Event;

            public void Awake(T payLoad) => Event?.Invoke(payLoad);
            public void Add(Action<T> action) => Event += action;
            public void Remove(Action<T> action) => Event -= action;
        }
    }
}