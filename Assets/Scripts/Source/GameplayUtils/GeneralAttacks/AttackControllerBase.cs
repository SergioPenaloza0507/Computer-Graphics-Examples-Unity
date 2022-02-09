using UnityEngine;

public abstract class AttackControllerBase : MonoBehaviour
{
    public abstract void InitAttack();
    public abstract void SolveAttack();
    public abstract void EndAttack();
}
