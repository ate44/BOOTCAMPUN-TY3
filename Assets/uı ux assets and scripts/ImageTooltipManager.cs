using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageTooltipManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipImage != null)
        {
            tooltipImage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipImage != null)
        {
            tooltipImage.SetActive(false);
        }
    }
}

