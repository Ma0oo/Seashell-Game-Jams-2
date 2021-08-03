using System;

namespace Infrastructure.ScenesServices.MainMenuPart.Attribute
{
    public class DataSlider : DataAttribute
    {
        public float Min { get; }
        public float Max { get; }
        public bool IsWholeNumber { get; private set; }

        public DataSlider(string nameProperty, float min, float max) : base(nameProperty)
        {
            if(min>max)
                throw new Exception("Oncorrect input");
            Min = min;
            Max = max;
            IsWholeNumber = false;
        }

        public DataSlider(string nameProperty,  int min, int max) : base(nameProperty)
        {
            if(min>max)
                throw new Exception("Oncorrect input");
            Min = min;
            Max = max;
            IsWholeNumber = true;
        }
    }
}