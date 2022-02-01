using System;

[Serializable]
public struct ButtonBinding
{
    public string name;
    public string buttonId;

    public ButtonBinding(string name, string buttonId)
    {
        this.name = name;
        this.buttonId = buttonId;
    }
}
