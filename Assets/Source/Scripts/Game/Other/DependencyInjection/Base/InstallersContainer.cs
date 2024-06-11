using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class InstallersContainer : SerializedMonoBehaviour
    {
        [field: SerializeField] public List<IInstaller> ServicesInstallers { get; private set; } = new();
    }
}