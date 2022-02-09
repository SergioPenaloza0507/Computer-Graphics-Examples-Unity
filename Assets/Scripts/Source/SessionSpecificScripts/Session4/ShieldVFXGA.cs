using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ShieldVFXGA : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play(float radius, float duration, float speed)
    {
        transform.localScale = Vector3.one * (radius / 4); 
        ParticleSystem.MainModule main = particleSystem.main;
        main.simulationSpeed = main.startLifetime.constantMax / (duration / speed);
        particleSystem.Play(true);
    }
}
