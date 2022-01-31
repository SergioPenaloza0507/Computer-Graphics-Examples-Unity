using System;
using UnityEngine;

namespace Spawners
{
    [Serializable]
    public struct SpawnerSource
    {
        [Tooltip("Id for the pool to find this source")]public string nameId;
        [Tooltip("Prefab to be created on pool allocation")]public GameObject prefab;
        [Tooltip("Start count for initial allocation")]public int startCount;
        [Tooltip("Whether or not this source will be shared by multiple spawners")]public bool shared;
    }
}
