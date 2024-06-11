using UnityEngine;

namespace MonoPool
{
    public interface IMonoPoolService
    {
        MonoPoolable Get<T>(Vector3 position = default) where T : MonoPoolable;

        void Release<T>(MonoPoolable monoPoolable) where T : MonoPoolable;
    }
}