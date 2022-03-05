using UnityEngine;
using UnityEngine.Animations;

public class ArchimedeanSpiral : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float travelSpeed;
    [SerializeField] private Axis planeNormal;
    [SerializeField] private float startAngle;
    [SerializeField] private float alpha;
    private float phi;

    private void Start()
    {
        phi = Mathf.Deg2Rad * startAngle;
    }

    void Update()
    {
        UpdateArchimedean();
    }

    /// <summary>
    /// Archimedean spiral
    /// radius = αφ
    /// a alpha = loop separation constant
    /// φ Phi   = polar angle
    /// </summary>
    private void UpdateArchimedean()
    {
        Vector3 targetLocalVector = target.localPosition;
        targetLocalVector.CartesianToPolarPlanar(planeNormal);
        switch (planeNormal)
        {
            case Axis.X:
                targetLocalVector.Set(targetLocalVector.x, alpha * phi, phi);
                break;
            case Axis.Y:
                targetLocalVector.Set(alpha * phi, targetLocalVector.y, phi);
                break;
            case Axis.Z:
                targetLocalVector.Set(alpha * phi, phi, targetLocalVector.z);
                break;
        }
        targetLocalVector.PolarToCartesianPlanar(planeNormal);
        target.localPosition = targetLocalVector;
        phi += Time.deltaTime * travelSpeed;
    }
}
