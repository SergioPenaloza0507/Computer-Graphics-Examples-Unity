using System;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PointAndClickAnimationController : MonoBehaviour
{
    private const string SPEED_PARAMETER_NAME = "Speed";

    private int speedParameterId;

    private Animator anim;

    private bool isMoving;
    
    private Vector3 lastPos;
    private static readonly int speed = Animator.StringToHash(SPEED_PARAMETER_NAME);
    [SerializeField] private float currentSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat(speed, 0);
    }

    private void Update()
    {
        if (isMoving)
        {
            var position = transform.position;
            currentSpeed = (position - lastPos).magnitude / Time.deltaTime;
            anim.SetFloat(speed, currentSpeed);
            lastPos = position;
        }
    }

    public void OnStartedMoving()
    {
        isMoving = true;
    }

    public void OnStopped()
    {
        isMoving = false;
        anim.SetFloat(speed, 0);
    }
}
