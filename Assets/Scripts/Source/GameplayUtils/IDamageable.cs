public interface IDamageable<TDamage, TMessage, TFaction> where TDamage : struct
{
    TMessage Damage(TDamage damage, TFaction faction);

    TMessage Die();
}
