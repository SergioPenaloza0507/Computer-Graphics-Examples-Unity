using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBAGame.Player
{
    public class MOBAPlayerAttackHandler : MonoBehaviour
    {
        [SerializeField] private AttackControllerBase qAttack;
        [SerializeField] private AttackControllerBase wAttack;
        [SerializeField] private AttackControllerBase eAttack;
        [SerializeField] private AttackControllerBase rAttack;
        public enum AttackType
        {
            Q,
            W,
            E,
            R
        }
        
        public struct AttackInfo
        {
            public AttackType attackType;
            public float attackDuration;

            public AttackInfo(AttackType attackType, float attackDuration)
            {
                this.attackType = attackType;
                this.attackDuration = attackDuration;
            }
        }

        [SerializeField] private ParticleSystem attackVFX;
        
        void Input_Attack_Q(InputButtonInfo info)
        {
            if (info.ButtonState != ButtonState.Press) return;
            SendMessage("OnAttack", new AttackInfo(AttackType.Q, 1.5f), SendMessageOptions.DontRequireReceiver);
            attackVFX.Play(true);
        }

        void Input_Attack_W(InputButtonInfo info)
        {
            
        }
    }
}
