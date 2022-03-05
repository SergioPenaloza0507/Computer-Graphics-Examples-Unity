using UnityEngine;
using UnityEngine.Animations;

public class FermatSpiral : MonoBehaviour
{
    [SerializeField] private Transform aTarget;
    [SerializeField] private Transform bTarget;
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
        UpdateFermat();
    }
    
    void UpdateFermat()
    {
        Vector3 aTargetLocalVector = aTarget.localPosition;
        Vector3 bTargetLocalVector = bTarget.localPosition;
        aTargetLocalVector.CartesianToPolarPlanar(planeNormal);
        bTargetLocalVector.CartesianToPolarPlanar(planeNormal);
        switch (planeNormal)
        {
            case Axis.X:
                aTargetLocalVector.Set(aTargetLocalVector.x, alpha * Mathf.Sqrt(phi), phi);
                bTargetLocalVector.Set(aTargetLocalVector.x, alpha * -Mathf.Sqrt(phi), phi);
                break;
            case Axis.Y:
                aTargetLocalVector.Set(alpha * Mathf.Sqrt(phi), aTargetLocalVector.y, phi);
                bTargetLocalVector.Set(alpha * -Mathf.Sqrt(phi), aTargetLocalVector.y, phi);
                break;
            case Axis.Z:
                aTargetLocalVector.Set(alpha * Mathf.Sqrt(phi), phi, aTargetLocalVector.z);
                bTargetLocalVector.Set(alpha * -Mathf.Sqrt(phi), phi, aTargetLocalVector.z);
                break;
        }
        aTargetLocalVector.PolarToCartesianPlanar(planeNormal);
        bTargetLocalVector.PolarToCartesianPlanar(planeNormal);
        aTarget.localPosition = aTargetLocalVector;
        bTarget.localPosition = bTargetLocalVector;
        phi += Time.deltaTime * travelSpeed;
    }
}
