using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class ComplexVisualEffect : IVisualEffect
    {
        [SerializeField] private List<IVisualEffect> _visualEffects = new();

        public bool IsExecuting => _visualEffects.Any(effect => effect.IsExecuting);
        
        public UniTask Execute()
        {
            return UniTask.WhenAll(_visualEffects.Select(ToExecution));

            UniTask ToExecution(IVisualEffect visualEffect)
            {
                return visualEffect.Execute();
            }
        }
    }
}