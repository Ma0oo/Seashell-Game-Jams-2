using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Plugins.HabObject.Property;

namespace HabObjects.Actors.Data
{
    public class ClassActor : DataProperty, ISaveDataPlayer
    {
        public Class ClassType;
        
        public enum Class
        {
            Arch, Knight, Magic
        }

        public void Save(DataPlayer data) => data.Class = ClassType;

        public void Load(DataPlayer data) => ClassType = data.Class;
    }
}