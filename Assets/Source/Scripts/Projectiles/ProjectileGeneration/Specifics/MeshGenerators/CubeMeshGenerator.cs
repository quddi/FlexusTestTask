using Extensions;
using UnityEngine;

namespace ProjectileGeneration
{
    public class CubeMeshGenerator : IMeshGenerator
    {
        [SerializeField] private float _sideLength;
        [SerializeField] private float _shiftDelta;
        
        private static readonly int[] Triangles = 
        { 
            0, 4, 1,
            1, 4, 5,
            1, 5, 2,
            2, 5, 6,
            2, 6, 3,
            6, 7, 3,
            3, 7, 0,
            4, 0, 7,
            4, 7, 5,
            5, 7, 6,
            0, 1, 3,
            1, 2, 3
        };
        
        public Mesh GetNext()
        {
            var mesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(-_sideLength / 2, -_sideLength / 2, -_sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(_sideLength / 2, -_sideLength / 2, -_sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(_sideLength / 2, -_sideLength / 2, _sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(-_sideLength / 2, -_sideLength / 2, _sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(-_sideLength / 2, _sideLength / 2, -_sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(_sideLength / 2, _sideLength / 2, -_sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(_sideLength / 2, _sideLength / 2, _sideLength / 2).ShiftRandomly(_shiftDelta),
                    new Vector3(-_sideLength / 2, _sideLength / 2, _sideLength / 2).ShiftRandomly(_shiftDelta),
                },
                triangles = Triangles
            };

            mesh.RecalculateNormals();

            return mesh;
        }
    }
}