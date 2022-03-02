using UnityEngine;

public class DummyDamageable : MonoBehaviour, IDamageable<float, DamageMessage, int>
{
    public DamageMessage Damage(float damage, int faction)
    {
        return DamageMessage.Damage;
    }

    public DamageMessage Die()
    {
        return DamageMessage.LethalDamage;
    }
}
