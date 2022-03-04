using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Spirals : MonoBehaviour
{
    private float radius, phi;

    [SerializeField] private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateArchimedean(target, Axis.Y, ref phi, ref radius);
    }

    void CartesianToPolarPlanar(ref Vector3 cartesianPosition, Axis planeNormal)
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

    void PolarToCartesianPlanar(ref Vector3 polarPosition, Axis planeNormal)
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
    
    void UpdateArchimedean(Transform target, Axis planeNormal, ref float phi, ref float radius)
    {
        //r = aφ
        Vector3 targetLocalVector = target.localPosition;
        CartesianToPolarPlanar(ref targetLocalVector, planeNormal);
        targetLocalVector.Set(phi * radius, targetLocalVector.y, phi);
        PolarToCartesianPlanar(ref targetLocalVector, planeNormal);
        target.localPosition = targetLocalVector;
        phi += Time.deltaTime;
        radius = Mathf.Clamp(radius - Time.deltaTime, 0.0f, Mathf.Infinity);
    }

    void UpdateHyperbolic()
    {
        
    }

    void UpdateFermat()
    {
        
    }

    void UpdateLituus()
    {
        
    }

    void UpdateLogarithmic()
    {
        
    }
}
