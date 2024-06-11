using Sirenix.OdinInspector;

namespace MonoPool
{
    public abstract class MonoPoolable : SerializedMonoBehaviour
    {
        public abstract void ResetState();
    }
}