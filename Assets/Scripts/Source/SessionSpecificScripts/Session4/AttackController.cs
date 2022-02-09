using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    [SerializeField] private ShieldVFXGA particleSystem;
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    [SerializeField] private float speed;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Q"))
        {
            anim.SetTrigger("Attack_Q");
            anim.SetFloat("Speed_Q", 2 / (duration / speed));
            particleSystem.Play(radius, duration, speed);
        }
    }
}
