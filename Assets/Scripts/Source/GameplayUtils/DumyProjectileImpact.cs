using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DumyProjectileImpact : MonoBehaviour
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void OnImpact(Vector3 position, Vector3 normal)
    {
        transform.parent = null;
        transform.position = position;
        transform.forward = normal;
        particles.Play(true);
    }
}
