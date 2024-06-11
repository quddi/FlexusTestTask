using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public abstract class PositioningVisualEffect : IVisualEffect
    {
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _xCurve = new();
        [SerializeField] private AnimationCurve _yCurve = new();
        [SerializeField] private AnimationCurve _zCurve = new();
        [SerializeField] protected Transform _animatedTransform;

        public bool IsExecuting { get; private set; }

        public async UniTask Execute()
        {
            if (IsExecuting) return;
            
            IsExecuting = true;
            var time = 0f;
            var startPosition = _animatedTransform.localPosition;
            
            while (time < _duration)
            {
                var t = time / _duration;
                var shift = new Vector3(_xCurve.Evaluate(t), _yCurve.Evaluate(t), _zCurve.Evaluate(t));

                _animatedTransform.localPosition = startPosition + shift;

                await UniTask.DelayFrame(1);

                time += Time.deltaTime;
            }
            
            _animatedTransform.localPosition = startPosition;
            IsExecuting = false;
        }
    }
}