using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayUtils.Tangible
{
    /// <summary>
    /// Helper component associated to the "DiegeticInstruction" prefab.
    /// A simple prompt in a world space canvas that you can close
    /// </summary>
    public class DiegeticInstruction : MonoBehaviour
    {
        #region Fields
        [SerializeField] private bool startActive;
        [SerializeField] private CinemachineVirtualCamera targetCamera;
        [SerializeField] private Button goBackButton;
        [SerializeField] private CanvasGroup canvasGroup;
        #endregion
        
        private void SetFocus()
        {
            targetCamera.gameObject.SetActive(true);
            canvasGroup.LeanAlpha(1f, 0.2f);
        }

        private void StopFocus()
        {
            targetCamera.gameObject.SetActive(false);
            canvasGroup.LeanAlpha(0f, 0.2f);
        }

        private void Awake()
        {
            targetCamera.gameObject.SetActive(startActive);
            goBackButton.onClick.AddListener(StopFocus);
        }

        private void OnMouseDown()
        {
            SetFocus();
        }
    }
}
