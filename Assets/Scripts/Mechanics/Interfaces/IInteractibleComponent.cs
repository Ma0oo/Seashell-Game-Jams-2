using System;

namespace Mechanics.Interfaces
{
    public interface IInteractbleComponent
    {
        public HabObject HabObject { get; }
        public bool IsActive { get; }
    }
}