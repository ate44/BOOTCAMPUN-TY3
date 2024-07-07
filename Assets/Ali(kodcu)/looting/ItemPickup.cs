using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private ItemController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<ItemController>();
            if (playerController != null)
            {
                playerController.groundItem = gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.groundItem = null;
            }
        }
    }
}
