using System;

namespace UI
{
    public interface IInputZone
    {
        bool IsInputActive { get; }

        event Action OnPointerReleased;
    }
}