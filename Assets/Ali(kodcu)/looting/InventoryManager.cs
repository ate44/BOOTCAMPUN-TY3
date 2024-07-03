using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Text potion1Text;
    public Text potion2Text;

    private int potion1Count = 0;
    private int potion2Count = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPotion(string potionType)
    {
        if (potionType == "Potion1")
        {
            potion1Count++;
            potion1Text.text = "Potion1: " + potion1Count;
        }
        else if (potionType == "Potion2")
        {
            potion2Count++;
            potion2Text.text = "Potion2: " + potion2Count;
        }
    }
}
