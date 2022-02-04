using System;
using System.Collections;
using System.Collections.Generic;
using DirectHierarchyMessaging;
using UnityEngine;

namespace MOBAGame.Player
{
    public delegate void OnAttackDelegate(ref MOBAPlayerAttackHandler.AttackInfo info);

    public delegate void OnBuffDelegate(ref float f);

    public class MOBAPlayerNotificationCenter : HierarchyNotificationCenter
    {
        private OnAttackDelegate onAttack;

        private OnBuffDelegate onBuff;
        public override void Notify<TMessage, TMessageParameters>(ref TMessageParameters parameters)
        {
            if (typeof(TMessage) == typeof(OnAttackDelegate))
            {
                if (parameters is MOBAPlayerAttackHandler.AttackInfo info)
                {
                    onAttack?.Invoke(ref info);
                }
            }
            
            if (typeof(TMessage) == typeof(OnBuffDelegate))
            {
                if (parameters is float info)
                {
                    onBuff?.Invoke(ref info);
                }
            }
        }
    }
}
