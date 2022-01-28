using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private GameObject activeCollider;


    private bool isFiring;

    private void Awake()
    {
        activeCollider.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isFiring)
            {
                isFiring = true;
                BroadcastMessage("FireStart", SendMessageOptions.DontRequireReceiver);
                Invoke(nameof(Fire), 0.5f);
                Invoke(nameof(StopFiring), 2.5f);
            }
        }
    }

    private void Fire()
    {
        activeCollider.SetActive(true);
    }

    private void StopFiring()
    {
        isFiring = false;
        activeCollider.SetActive(false);
    }
}
