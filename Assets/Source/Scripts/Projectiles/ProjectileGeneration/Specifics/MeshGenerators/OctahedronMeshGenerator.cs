using Extensions;
using UnityEngine;

namespace ProjectileGeneration
{
    public class OctahedronMeshGenerator : IMeshGenerator
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _shiftDelta;

        private static readonly int[] Triangles = 
        { 
            0, 4, 1,
            1, 4, 2,
            2, 4, 3,
            3, 4, 0,
            0, 1, 5,
            1, 2, 5,
            2, 3, 5,
            3, 0, 5
        };
        
        public Mesh GetNext()
        {
            var mesh = new Mesh
            {
                vertices = new[]
                {
                    new Vector3(-_radius, 0, 0).ShiftRandomly(_shiftDelta),
                    new Vector3(0, 0, -_radius).ShiftRandomly(_shiftDelta),
                    new Vector3(_radius, 0, 0).ShiftRandomly(_shiftDelta),
                    new Vector3(0, 0, _radius).ShiftRandomly(_shiftDelta),
                    new Vector3(0, _radius, 0).ShiftRandomly(_shiftDelta),
                    new Vector3(0, -_radius, 0).ShiftRandomly(_shiftDelta),
                },
                triangles = Triangles
            };

            mesh.RecalculateNormals();

            return mesh;
        }
    }
}