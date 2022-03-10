using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ArchimedeanSpiralG2 : MonoBehaviour
{
    [SerializeField] private float b;
    [SerializeField][Range(0, 360)] private float speed;
    [SerializeField] private Transform target;
    private float theta = 0;
    
    void Update()
    {
        theta += Time.deltaTime * speed * Mathf.Deg2Rad;
        SolveSpiral();
    }

    void CartesianToPolarY(ref Vector3 cartesianPosition)
    {
        cartesianPosition.Set
        (
        Mathf.Sqrt
            (
            Mathf.Pow(cartesianPosition.x, 2) + Mathf.Pow(cartesianPosition.z, 2)
            ),
            cartesianPosition.y,
            Mathf.Atan2(cartesianPosition.z, cartesianPosition.x)
        );
    }

    void PolarToCartesianY(ref Vector3 polarCoords)
    {
        polarCoords.Set
        (
            polarCoords.x * Mathf.Cos(polarCoords.z),
            polarCoords.y,
            polarCoords.x * Mathf.Sin(polarCoords.z)
        );
    }

    void SolveSpiral()
    {
        Vector3 localPosition = target.localPosition;
        CartesianToPolarY(ref localPosition);
        Debug.Log($"Polar: {localPosition}");
        localPosition.Set(Mathf.Clamp(b * localPosition.z, -1, 0.1f), localPosition.y, theta);
        PolarToCartesianY(ref localPosition);
        Debug.Log($"Cartesian: {localPosition}");
        target.localPosition = localPosition;
    }
}
