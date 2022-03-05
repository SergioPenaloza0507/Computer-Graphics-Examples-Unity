using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]
public class GravitySolver : MonoBehaviour
{
    [SerializeField] private float influenceRadius;
    [SerializeField] private LayerMask influenceMask;
    [SerializeField] private int influencedObjectLimit;
    [SerializeField] private float gravitationalConstant;
    [SerializeField] private float maxForceMagnitude;

    private Collider[] influencedColliders;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        influencedColliders = new Collider[influencedObjectLimit];
    }

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, influenceRadius, influencedColliders, influenceMask);
        for (int i = 0; i < influencedColliders.Length; i++)
        {
            Collider col = influencedColliders[i];
            if (col == null) continue;
            if (col.TryGetComponent(out Rigidbody otherRb))
            {
                Vector3 direction = (transform.position - otherRb.transform.position);
                otherRb.AddForce(
                    direction.normalized * 
                    Mathf.Clamp(gravitationalConstant * rb.mass * otherRb.mass / Mathf.Pow(direction.magnitude, 2), 0, maxForceMagnitude),
                    ForceMode.Impulse
                );
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, influenceRadius);
    }
#endif
}
