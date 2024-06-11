using System;

namespace UI
{
    public interface IFirePowerProvider
    {
        float RelativePower { get; }

        event Action<float> OnPowerChanged;
    }
}