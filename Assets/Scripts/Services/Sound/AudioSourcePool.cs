using System.Collections.Generic;
using UnityEngine;

namespace Services.Sound
{
    internal class AudioSourcePool
    {
        private Stack<AudioSource> _free = new Stack<AudioSource>();
        private List<AudioSource> _busy = new List<AudioSource>();

        private Transform _parentSource;
        private AudioSource _template;
        
        public AudioSourcePool(int startCount, Transform parent, AudioSource template)
        {
            Object.DontDestroyOnLoad(parent);
            _parentSource = parent;
            _template = template;
            for (int i = 0; i < startCount; i++) AddNew(Object.Instantiate(template));
        }

        public void AddNew(AudioSource template)
        {
            var sourceOnScene = Object.Instantiate(template, _parentSource, true);
            Object.DontDestroyOnLoad(sourceOnScene);
            _free.Push(sourceOnScene);
            sourceOnScene.enabled = false;
        }

        public void Return(AudioSource source)
        {
            if(!_busy.Remove(source))
                return;
            _free.Push(source);
            source.Stop();
            source.transform.SetParent(_parentSource);
            source.enabled = false;
        }

        public AudioSource GetFree()
        {
            if(_free.Count==0)
                AddNew(_template);
            
            var result = _free.Pop();
            _busy.Add(result);
            result.enabled = true;
            return result;
        }

        public void OffAll()
        {
            foreach (var source in _busy) Return(source);
        }
    }
}