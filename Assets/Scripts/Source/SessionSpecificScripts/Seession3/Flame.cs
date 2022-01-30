using UnityEngine;
using UnityEngine.Serialization;

public class Flame : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float cycleTimer = 0.2f;

    private float timer;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable<float, DamageMessage, int> damageable))
        {
            timer += Time.deltaTime;
            if (timer > cycleTimer)
            {
                damageable.Damage(damage, 0);
                timer = 0;
            }
        }
    }
}
