using System;

namespace Plugins.HubObject.GlobalSystem
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DI : Attribute 
    {
        public string Id { get; }
        public DI(string id = "") => Id = id;
    }
}