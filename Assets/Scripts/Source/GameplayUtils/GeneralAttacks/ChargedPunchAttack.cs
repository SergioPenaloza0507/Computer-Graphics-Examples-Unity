using UnityEngine;

public class ChargedPunchAttack : AttackControllerBase
{
    enum State
    {
        Waiting,
        Ready,
        Performing
    }
    
    [SerializeField] private float duration;
    [SerializeField] private float animationDuration;
    [SerializeField] private float cooldown;

    private State state;
    private float attackTimer;
    private float cooldownTimer;

    private void Update()
    {
        if (state == State.Waiting)
        {
            if (cooldownTimer > cooldown)
            {
                state = State.Ready;
            }
            cooldownTimer += Time.deltaTime;
        }
    }

    public override void InitAttack()
    {
        if (state != State.Ready) return;
        attackTimer = 0;
        state = State.Performing;
    }

    public override void SolveAttack()
    {
        if (state != State.Performing) return;
        attackTimer += Time.deltaTime;
        if (attackTimer > duration)
        {
            state = State.Waiting;
            attackTimer = 0;
        }
    }

    public override void EndAttack()
    {
        if (state != State.Performing) return;
        state = State.Waiting;
    }
}
