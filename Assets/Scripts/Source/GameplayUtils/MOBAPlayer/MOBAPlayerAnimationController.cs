using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace MOBAGame.Player
{
    [RequireComponent(typeof(Animator))]
    public class MOBAPlayerAnimationController : MonoBehaviour
    {
        private const string SPEED_PARAMETER_NAME = "Speed";

        private int speedParameterId;

        private Animator anim;

        private bool isMoving;

        private Vector3 lastPos;
        private static readonly int speed = Animator.StringToHash(SPEED_PARAMETER_NAME);
        private float currentSpeed;

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

        public void OnAttack(MOBAPlayerAttackHandler.AttackInfo attackInfo)
        {
            switch (attackInfo.attackType)
            {
                case MOBAPlayerAttackHandler.AttackType.Q:
                    anim.SetTrigger("Attack_Q");
                    StartCoroutine(ResetTriggerAfterFrame("Attack_Q"));
                    break;
                case MOBAPlayerAttackHandler.AttackType.W:
                    anim.SetTrigger("Attack_W");
                    StartCoroutine(ResetTriggerAfterFrame("Attack_W"));
                    break;
                case MOBAPlayerAttackHandler.AttackType.E:
                    anim.SetTrigger("Attack_E");
                    StartCoroutine(ResetTriggerAfterFrame("Attack_E"));
                    break;
                case MOBAPlayerAttackHandler.AttackType.R:
                    anim.SetTrigger("Attack_R");
                    StartCoroutine(ResetTriggerAfterFrame("Attack_R"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        IEnumerator ResetTriggerAfterFrame(string triggerName)
        {
            yield return new WaitForEndOfFrame();
            anim.ResetTrigger(triggerName);
        }
    }
}
