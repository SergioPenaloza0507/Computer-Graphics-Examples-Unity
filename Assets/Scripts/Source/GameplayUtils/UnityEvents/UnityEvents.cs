using System;
using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
    [Serializable]
    public class DualVectorEvent : UnityEvent<Vector3, Vector3>
    {
        
    }
}
