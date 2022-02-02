using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBAGame.Player
{
    public class MOBAPlayerAttackHandler : MonoBehaviour
    {
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
        
        void Input_Attack_Q()
        {
            SendMessage("OnAttack", new AttackInfo(AttackType.Q, 1.5f), SendMessageOptions.DontRequireReceiver);
            attackVFX.Play(true);
        }
    }
}
