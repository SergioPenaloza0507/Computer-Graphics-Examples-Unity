public struct InputButtonInfo
{
    #region Fields
    private ButtonState buttonState;
    private object extraData;
    #endregion
    
    #region Properties
    public ButtonState ButtonState
    {
        get => buttonState;
        set => buttonState = value;
    }

    public object ExtraData
    {
        get => extraData;
        set => extraData = value;
    }
    #endregion

    public InputButtonInfo(ButtonState buttonState, object extraData)
    {
        this.buttonState = buttonState;
        this.extraData = extraData;
    }

    public TData GetData<TData>() 
    {
        if (extraData is TData data)
        {
            return data;
        }
        return default;
    }
    
}
