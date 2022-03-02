using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class ProyectileDummy : MonoBehaviour
{
    [SerializeField] 
    private float dmg;
    [SerializeField]
    private float lifetime;
    

    [SerializeField]
    private CustomEvents.DualVectorEvent onImpact;
    
    private TrailRenderer rend;

    private void Awake()
    {
        rend = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        rend.Clear();
        Invoke(nameof(Disable), lifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable<float, DamageMessage, int> damageable))
        {
            damageable.Damage(dmg, 0);
            onImpact?.Invoke(other.contacts[0].point, other.contacts[0].normal);
            Debug.Log("Impact!");
            ParticleSystem a;
        }
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
}
