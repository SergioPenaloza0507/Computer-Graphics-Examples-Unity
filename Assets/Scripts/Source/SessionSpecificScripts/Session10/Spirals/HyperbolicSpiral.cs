using UnityEngine;
using UnityEngine.Animations;

public class HyperbolicSpiral : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float travelSpeed;
    [SerializeField] private Axis planeNormal;
    [SerializeField] private float startAngle;
    [SerializeField] private float alpha;

    private float phi;
    void Start()
    {
        phi = Mathf.Deg2Rad * startAngle;
    }

    void Update()
    {
        UpdateHyperbolic();
    }
    
    /// <summary>
    /// Hyperbolic Spiral
    /// r = a / φ
    /// a alpha = loop separation constant
    /// φ Phi   = polar angle
    /// </summary>
    void UpdateHyperbolic()
    {
        Vector3 targetLocalVector = target.localPosition;
        targetLocalVector.CartesianToPolarPlanar(planeNormal);
        switch (planeNormal)
        {
            case Axis.X:
                targetLocalVector.Set(targetLocalVector.x, alpha / phi, phi);
                break;
            case Axis.Y:
                targetLocalVector.Set(alpha / phi, targetLocalVector.y, phi);
                break;
            case Axis.Z:
                targetLocalVector.Set(alpha / phi, phi, targetLocalVector.z);
                break;
        }
        targetLocalVector.PolarToCartesianPlanar(planeNormal);
        target.localPosition = targetLocalVector;
        phi += Time.deltaTime * travelSpeed;
    }
}
