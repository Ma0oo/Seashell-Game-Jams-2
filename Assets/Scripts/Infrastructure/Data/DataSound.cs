using Infrastructure.ScenesServices.MainMenuPart.Attribute;
using UnityEngine;

namespace Infrastructure.Data
{
    public class DataSound : IData
    {
        public string Name => "SoundData";
        [DataSlider("Master Volume", -80, 20)]public float Master = 0;
        [DataSlider("Music Volume", -80, 20)]public float Music = -38;
        [DataSlider("Effect Volume", -80, 20)]public float Effect = 0;
        [DataSlider("Ui Volume", -80, 20)]public float UI = -16;
    }
}