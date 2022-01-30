using System;
namespace InGameServices
{
    [Flags]
    public enum ServiceLocatorUnsafeFallback
    {
        CreateNew = 2,
        FindInScene = 4
    }
}
