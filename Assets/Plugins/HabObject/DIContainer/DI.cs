using System;

namespace Plugins.HabObject.DIContainer
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DI : Attribute 
    {
        public string Id { get; }
        public DI() => Id = "";
        public DI(string id = "") => Id = id;
    }
}