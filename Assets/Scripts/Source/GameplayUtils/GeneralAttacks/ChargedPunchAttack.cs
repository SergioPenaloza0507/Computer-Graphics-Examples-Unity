using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChargedPunchAttack : MonoBehaviour
{
    enum State
    {
        Ready,
        Performing
    }

    [SerializeField] private float duration;
    [SerializeField] private float speed;
    [SerializeField] private float animationDuration;
    [SerializeField] private float cooldown;
    [SerializeField] private PunchAttackVFX vfx;
    
    private State state;
    private float realSpeed;
    private float realDuration;
    private float attackTimer;
    private Animator anim;

    private void ComputeRealSpeed()
    {
        realDuration = duration / speed;
        realSpeed = realDuration / animationDuration;
    }

    private void OnValidate()
    {
        ComputeRealSpeed();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetButtonDown("W"))
                {
                    state = State.Performing;
                    vfx.Play();
                    anim.SetTrigger("Attack_W");
                }
                break;
            case State.Performing:
                if (attackTimer > realDuration)
                {
                    state = State.Ready;
                    attackTimer = 0;
                }

                attackTimer += Time.deltaTime;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
