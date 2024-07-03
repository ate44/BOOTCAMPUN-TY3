using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            Potion potion = hit.collider.GetComponent<Potion>();
            if (potion != null)
            {
                InventoryManager.Instance.AddPotion(potion.potionType);
                Destroy(potion.gameObject);
            }
        }
    }
}
