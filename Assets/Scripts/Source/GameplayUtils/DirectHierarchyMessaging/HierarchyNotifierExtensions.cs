using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectHierarchyMessaging
{
    public static class HierarchyNotifierExtensions
    {
        public static void Notify<TMessage, TMessageParameters>(this IHierarchyNotifier notifier, ref TMessageParameters parameters)
            where TMessage : Delegate where TMessageParameters : struct
        {
            notifier.NotificationCenter.Notify<TMessage, TMessageParameters>(ref parameters);
        }
    }
}
