using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Transform handTransform; // Karakterin eli
    public GameObject currentItem; // Karakterin �u anki itemi
    public GameObject groundItem; // Yerdeki item
    public bool canSwapItems = false; // Item de�i�tirme izni

    void Update()
    {
        if (canSwapItems && Input.GetKeyDown(KeyCode.E) && groundItem != null)
        {
            SwapItems();
        }
    }

    void SwapItems()
    {
        if (currentItem != null && groundItem != null)
        {
            // �u anki itemi yere b�rak
            currentItem.transform.SetParent(null);
            currentItem.transform.position = groundItem.transform.position;
            currentItem.transform.rotation = groundItem.transform.rotation;

            // Yerdeki itemi elimize al
            groundItem.transform.SetParent(handTransform);
            groundItem.transform.localPosition = Vector3.zero;
            groundItem.transform.localRotation = Quaternion.identity;

            // Item referanslar�n� g�ncelle
            GameObject temp = currentItem;
            currentItem = groundItem;
            groundItem = temp;
        }
    }
}
