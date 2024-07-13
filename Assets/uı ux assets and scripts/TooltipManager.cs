using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipText != null)
        {
            tooltipText.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipText != null)
        {
            tooltipText.SetActive(false);
        }
    }
}

