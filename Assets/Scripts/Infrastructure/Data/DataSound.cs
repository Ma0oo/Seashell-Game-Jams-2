using Infrastructure.Scenes.MainMenuPart.Attribute;

namespace Infrastructure.Data
{
    public class DataSound : IData
    {
        public string Name => "SoundData";
        [DataSlider("Master Volume", 0, 1f)]public float Master = 1;
        [DataSlider("Music Volume", 0, 1f)]public float Music = 1;
        [DataSlider("Effect Volume", 0, 1f)]public float Effect = 1;
    }
}