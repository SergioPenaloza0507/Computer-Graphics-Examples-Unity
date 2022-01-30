using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class ProyectileDummy : MonoBehaviour
{
    [SerializeField] private float dmg;

    private TrailRenderer rend;

    private void Awake()
    {
        rend = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        rend.Clear();
        Invoke(nameof(Disable), 1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable<float, DamageMessage, int> damageable))
        {
            damageable.Damage(dmg, 0);
        }
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
}
