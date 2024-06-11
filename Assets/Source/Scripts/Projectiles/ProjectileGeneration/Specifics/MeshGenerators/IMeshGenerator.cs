using UnityEngine;

namespace ProjectileGeneration
{
    public interface IMeshGenerator
    {
        Mesh GetNext();
    }
}