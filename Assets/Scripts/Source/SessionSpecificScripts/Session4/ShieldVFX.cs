
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ShieldVFX : MonoBehaviour
{
    private const float REAL_RADIUS = 4f;
    
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    [SerializeField] private float speed;

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule main;
    private int state = 0;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        main = particleSystem.main;
    }

    private void Update()
    {
        if (!particleSystem.isPlaying && Input.GetButtonDown("Jump"))
        {
            Play();
        }
    }

    private void OnParticleSystemStopped()
    {
        ResetSpeed();
        ResetRadius();
    }

    private void SetSpeed()
    {
        main.simulationSpeed = (duration / speed) / (main.duration + main.startLifetime.constantMax);
    }

    private void SetRadius()
    {
        transform.localScale = Vector3.one * (radius / REAL_RADIUS);
    }

    private void ResetRadius()
    {
        transform.localScale = Vector3.one;
    }

    private void ResetSpeed()
    {
        main.simulationSpeed = 1.0f;
    }

    public void Play()
    {
        SetSpeed();
        SetRadius();
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play(true);
        }
    }
}
