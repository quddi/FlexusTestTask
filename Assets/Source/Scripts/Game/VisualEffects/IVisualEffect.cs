using Cysharp.Threading.Tasks;

namespace Game
{
    public interface IVisualEffect
    {
        bool IsExecuting { get; }
        
        UniTask Execute();
    }
}