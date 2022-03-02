using System;
using System.Collections.Generic;
using Spawners;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SphereCollider))]
public class DummyTurret : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private AimConstraint[] targetingConstraints;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float shootForce = 1000;

    private float fireTimer;
    [SerializeField]private Transform target;

    private void Fire()
    {
        var position = muzzle.position;
        Rigidbody rb = Instantiate(prefab, position, muzzle.transform.rotation).GetComponent<Rigidbody>();
        rb.gameObject.SetActive(true);
        rb.AddForce((target.position - position).normalized * shootForce);
    }

    private void Update()
    {
        if (target != null)
        {
            if (fireTimer > fireRate)
            {
                fireTimer = 0.0f;
                Fire();
            }

            fireTimer += Time.deltaTime;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (detectionMask == (detectionMask | (1 << other.gameObject.layer)))
        {
            if (target == null)
            {
                target = other.transform;
            }

            if (target == other.transform)
            {
                if (fireTimer > fireRate)
                {
                    fireTimer = 0.0f;
                    Fire();
                }

                fireTimer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectionMask == (detectionMask | (1 << other.gameObject.layer)))
        {
            if (other.transform == target)
            {
                target = null;
            }
        }
    }
}
