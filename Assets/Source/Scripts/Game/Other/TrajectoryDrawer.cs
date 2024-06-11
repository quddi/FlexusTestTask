using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private LineRenderer _lineRenderer;

        [SerializeField, TabGroup("Parameters")] private int _pointsCount;
        [SerializeField, TabGroup("Parameters")] private float _delta;
        
        [Button]
        public void RefreshTrajectory(Vector3 startPosition, Vector3 startSpeed)
        {
            _lineRenderer.positionCount = _pointsCount;

            for (int i = 0; i < _pointsCount; i++)
            {
                _lineRenderer.SetPosition(i, CalculatePositionAtTime(i * _delta, startPosition, startSpeed));
            }
        }

        private Vector3 CalculatePositionAtTime(float time, Vector3 startPosition, Vector3 startSpeed)
        {
            var position = startPosition + startSpeed * time;
            position.y += -Constants.GravityAccelerationValue * 0.5f * time * time;
            return position;
        }
    }
}