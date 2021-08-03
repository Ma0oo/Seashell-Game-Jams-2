using System;

namespace Infrastructure.ScenesServices.MainMenuPart.Attribute
{
    public class DataButton : DataAttribute
    {
        private int _defaultIndex;
        private int _currentIndex;
        private object[] _variants;

        public object DefaultValue => _variants[_defaultIndex];
        
        public DataButton(string nameProperty, int defaultIndex ,object[] variants) : base(nameProperty)
        {
            if(variants.Length < 2 || variants == null)
                throw  new Exception("No varrints");
            if(defaultIndex<0 || defaultIndex>=variants.Length)
                throw  new Exception("Uncorrect index");
            _defaultIndex = defaultIndex;
            _currentIndex = defaultIndex;
            _variants = variants;
        }

        public object Next()
        {
            _currentIndex++;
            if (_currentIndex >= _variants.Length)
                _currentIndex = 0;
            return _variants[_currentIndex];
        }
        
        public object Prev()
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _variants.Length - 1;
            return _variants[_currentIndex];
        }
    }
}