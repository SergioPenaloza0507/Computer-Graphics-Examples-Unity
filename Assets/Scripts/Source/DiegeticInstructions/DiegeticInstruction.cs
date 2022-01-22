using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class DiegeticInstruction : MonoBehaviour
{
    [SerializeField] private bool startActive;
    [SerializeField] private CinemachineVirtualCamera targetCamera;
    [SerializeField] private Button goBackButton;
    [SerializeField] private CanvasGroup canvasGroup;

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
