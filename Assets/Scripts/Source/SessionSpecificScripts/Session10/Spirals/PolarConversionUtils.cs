using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public static class PolarConversionUtils
{
    public static void CartesianToPolarPlanar(this ref Vector3 cartesianPosition, Axis planeNormal)
    {
        switch (planeNormal)
        {
            case Axis.X:
                cartesianPosition.Set(
                    cartesianPosition.x, 
                    Mathf.Sqrt(Mathf.Pow(cartesianPosition.y, 2) + Mathf.Pow(cartesianPosition.z, 2)),
                    Mathf.Atan2(cartesianPosition.z, cartesianPosition.y)
                    );
                break;
            case Axis.Y:
                cartesianPosition.Set(
                    Mathf.Sqrt(Mathf.Pow(cartesianPosition.x, 2) + Mathf.Pow(cartesianPosition.z, 2)),
                    cartesianPosition.y,
                    Mathf.Atan2(cartesianPosition.z, cartesianPosition.x)
                );
                break;
            case Axis.Z:
                cartesianPosition.Set(
                    Mathf.Sqrt(Mathf.Pow(cartesianPosition.x, 2) + Mathf.Pow(cartesianPosition.y, 2)),
                    Mathf.Atan2(cartesianPosition.y, cartesianPosition.x),
                    cartesianPosition.z
                );
                break;
        }
    }

    public static void PolarToCartesianPlanar(this ref Vector3 polarPosition, Axis planeNormal)
    {
        switch (planeNormal)
        {
            case Axis.X:
                polarPosition.Set(
                    polarPosition.x, 
                    polarPosition.y * Mathf.Cos(polarPosition.z),
                    polarPosition.y * Mathf.Sin(polarPosition.z)
                );
                break;
            case Axis.Y:
                polarPosition.Set(
                    polarPosition.x * Mathf.Cos(polarPosition.z),
                    polarPosition.y,
                    polarPosition.x * Mathf.Sin(polarPosition.z)
                );
                break;
            case Axis.Z:
                polarPosition.Set(
                    polarPosition.x * Mathf.Cos(polarPosition.y), 
                    polarPosition.x * Mathf.Sin(polarPosition.y),
                    polarPosition.z
                );
                break;
        }
    }
}
