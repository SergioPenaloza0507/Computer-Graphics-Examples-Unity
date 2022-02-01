using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool controlSelfOnAwake;
    [SerializeField] private InputBinder binder;

    private GameObject controlledObject;

    private void Awake()
    {
        if (controlSelfOnAwake)
        {
            Control(gameObject);
        }
    }

    private void Update()
    {
        if (controlledObject == null) return;
        binder.EvaluateBindings(this);
    }

    public void QueryButton(string buttonName, string binding)
    {
        if (Input.GetButtonDown(buttonName))
        {
            controlledObject.BroadcastMessage($"Input_{binding}", new InputButtonInfo(ButtonState.Press, Input.mousePosition), SendMessageOptions.DontRequireReceiver);
        }
        
        if (Input.GetButton(buttonName))
        {
            controlledObject.BroadcastMessage($"Input_{binding}", new InputButtonInfo(ButtonState.Keep, Input.mousePosition), SendMessageOptions.DontRequireReceiver);
        }
        
        if (Input.GetButtonUp(buttonName))
        {
            controlledObject.BroadcastMessage($"Input_{binding}", new InputButtonInfo(ButtonState.Release, Input.mousePosition), SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Control(GameObject g)
    {
        if(g == controlledObject) return;
        if (controlledObject != null)
        {
            ReleaseControl(controlledObject);
        }
        
        controlledObject = g;
        controlledObject.BroadcastMessage("Input_OnPlayerControl", SendMessageOptions.DontRequireReceiver);
    }

    public void ReleaseControl(GameObject g)
    {
        controlledObject.BroadcastMessage("Input_OnReleaseControl", SendMessageOptions.DontRequireReceiver);
        controlledObject = null;
    }
}
