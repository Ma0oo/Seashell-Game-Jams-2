using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Plugins.HabObject.Property;

namespace HabObjects.Actors.Data
{
    public class Special : DataProperty, ISaveDataPlayer
    {
        public int Force;
        public int Agility;
        public int Charisma;
        public int Magic;
        
        public void Save(DataPlayer data)
        {
            data.Force = Force;
            data.Agility = Agility;
            data.Charisma = Charisma;
            data.Magic = Magic;
        }

        public void Load(DataPlayer data)
        {
            Force = data.Force;
            Agility = data.Agility;
            Charisma = data.Charisma;
            Magic = data.Magic;
        }
    }
}