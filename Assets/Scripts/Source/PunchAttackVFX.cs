using Cinemachine;
using UnityEngine;

public class PunchAttackVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    public void Play()
    {
        particleSystem.Play(true);
    }

    public void Stop()
    {
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
