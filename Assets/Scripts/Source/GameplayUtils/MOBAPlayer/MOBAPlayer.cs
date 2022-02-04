using System;
using UnityEngine;

namespace MOBAGame.Player
{
    [RequireComponent(typeof(MOBAPlayerMovement))]
    [RequireComponent(typeof(MOBAPlayerAnimationController))]
    [RequireComponent(typeof(MOBAPlayerAttackHandler))]
    [RequireComponent(typeof(MOBAPlayerCameraManager))]
    public class MOBAPlayer : MonoBehaviour, IDamageable<float, DamageMessage, byte>
    {
        [SerializeField] private float maxHealth;

        private float health;
        
        private void Awake()
        {
            health = maxHealth;
        }

        public DamageMessage Damage(float damage, byte faction)
        {
            if (health <= 0) return DamageMessage.NoDamage;
            health = Mathf.Clamp(health - damage, 0, maxHealth);
            if (health <= 0)
            {
                return Die();
            }

            return DamageMessage.Damage;
        }

        public DamageMessage Die()
        {
            return DamageMessage.LethalDamage;
        }
    }
}
