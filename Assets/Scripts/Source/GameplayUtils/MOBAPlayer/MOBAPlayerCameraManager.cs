using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace MOBAGame.Player
{
    public class MOBAPlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private CinemachineVirtualCamera buffCamera;

        private void Awake()
        {
            mainCamera.transform.parent = null;
            buffCamera.transform.parent = null;
        }

        public void OnAttack(MOBAPlayerAttackHandler.AttackInfo info)
        {
            SetBuffCameraLive();
            Invoke(nameof(SetMainCameraLive), info.attackDuration);
        }

        private void SetBuffCameraLive()
        {
            mainCamera.gameObject.SetActive(false);
            buffCamera.gameObject.SetActive(true);
        }

        private void SetMainCameraLive()
        {
            mainCamera.gameObject.SetActive(true);
            buffCamera.gameObject.SetActive(false);
        }
    }
}
