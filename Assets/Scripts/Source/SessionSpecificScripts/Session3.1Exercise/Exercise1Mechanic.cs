using UnityEngine;

public class Exercise1Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject coll;

    private void Update()
    {
        if (coll.activeSelf) return; 
        if (Input.GetButtonDown("Jump"))
        {
            Invoke(nameof(ActivateCollider), 0.5f);
            BroadcastMessage("ShieldStart", SendMessageOptions.DontRequireReceiver);
            Invoke(nameof(DeactivateCollider), 3);
        }
    }

    private void ActivateCollider()
    {
        coll.SetActive(true);
    }

    private void DeactivateCollider()
    {
        coll.SetActive(false);
    }
}
