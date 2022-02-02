using System;
using UnityEngine;

namespace DirectHierarchyMessaging
{
    public abstract class HierarchyNotificationCenter : MonoBehaviour
    {
        public abstract void Notify<TMessage, TMessageParameters>(ref TMessageParameters parameters)
            where TMessage : Delegate where TMessageParameters : struct;

        protected void Awake()
        {
            foreach (IHierarchyNotifier child in GetComponentsInChildren<IHierarchyNotifier>())
            {
                child.NotificationCenter = this;
            }
        }
    }
}
