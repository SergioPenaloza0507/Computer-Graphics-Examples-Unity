using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainModule = UnityEngine.ParticleSystem.MainModule;

[RequireComponent(typeof(Animator))]
public class ChargedPunchAttackG1 : MonoBehaviour
{
    enum State
    {
        Ready,
        Performing
    }
    
    [SerializeField] private float duration;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem particleSystem;

    private State currentState = State.Ready;
    private float timer;
    private Animator anim;

    private int triggerHash;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentState == State.Ready)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Play effect
                anim.SetTrigger("Attack_W");
                anim.SetFloat("Speed_W", 2.167f / (duration/speed));
                SetParticlesSpeed();
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particleSystem.Play(true);
                currentState = State.Performing;
            }
        }

        if (currentState == State.Performing)
        {
            if (timer > duration / speed)
            {
                //Stop Effect
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                currentState = State.Ready;
                timer = 0;
            }

            timer += Time.deltaTime;
        }
    }

    void SetParticlesSpeed()
    {
        foreach (ParticleSystem particles in particleSystem.GetComponentsInChildren<ParticleSystem>())
        {
            MainModule main = particles.main;
            main.simulationSpeed = 2.167f / (duration / speed);
        }
    }
}
