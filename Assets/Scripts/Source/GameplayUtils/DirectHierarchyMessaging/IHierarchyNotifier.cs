using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectHierarchyMessaging
{
    public interface IHierarchyNotifier
    {
        HierarchyNotificationCenter NotificationCenter { get; set; }

        void RegisterCallback<TMessage>(TMessage m) where TMessage : Delegate;
    }
}
