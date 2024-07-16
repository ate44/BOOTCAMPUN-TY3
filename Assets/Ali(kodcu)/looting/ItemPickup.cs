using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController playerController = other.GetComponent<ItemController>();
            if (playerController != null)
            {
                playerController.groundItem = gameObject;
                playerController.canSwapItems = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemController playerController = other.GetComponent<ItemController>();
            if (playerController != null)
            {
                playerController.groundItem = null;
                playerController.canSwapItems = false;
            }
        }
    }
}
