using UnityEngine;

public abstract class InputBinder : ScriptableObject
{
    public abstract void EvaluateBindings(InputHandler handler);
}
