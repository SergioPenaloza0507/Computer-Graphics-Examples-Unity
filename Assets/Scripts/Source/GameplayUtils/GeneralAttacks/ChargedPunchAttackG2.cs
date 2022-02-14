using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainModule = UnityEngine.ParticleSystem.MainModule;

[RequireComponent(typeof(Animator))]
public class ChargedPunchAttackG2 : MonoBehaviour
{
    enum State
    {
        Ready,
        Performing
    }

    [SerializeField] private float duration;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem particleSystem;

    private float timer = 0f;
    private State state = State.Ready;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == State.Ready)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Play attack
                float computedSpeed = 2.167f / (duration / speed);
                anim.SetFloat("Speed_W", computedSpeed);
                anim.SetTrigger("Attack_W");
                SetParticlesSpeed(computedSpeed);
                particleSystem.Play(true);
                Debug.Break();
                state = State.Performing;
            }
        }

        if (state == State.Performing)
        {
            if (timer > duration / speed)
            {
                //Stop Particles
                timer = 0f;
                state = State.Ready;
            }

            timer += Time.deltaTime;
        }
    }

    void SetParticlesSpeed(float sp)
    {
        foreach (ParticleSystem system in particleSystem.GetComponentsInChildren<ParticleSystem>())
        {
            MainModule main = system.main;

            main.simulationSpeed = sp;
        }
    }
}
