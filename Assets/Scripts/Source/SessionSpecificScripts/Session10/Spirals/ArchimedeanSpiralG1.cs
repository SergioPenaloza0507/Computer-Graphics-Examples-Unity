using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ArchimedeanSpiralG1 : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField][Range(0,360)] private float speed;
    [SerializeField] private float b = 0.1f;
    private float angle;

    private void Update()
    {
        angle += Time.deltaTime * speed * Mathf.Deg2Rad;
        SolveSpiral();
    }
    
    //r = bt

    private void SolveSpiral()
    {
        Vector3 localPosition = target.localPosition;
        localPosition.CartesianToPolarPlanar(Axis.Y);
        localPosition.Set(b * angle, localPosition.y, angle);
        localPosition.PolarToCartesianPlanar(Axis.Y);
        target.localPosition = localPosition;
    }
}
