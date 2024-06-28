using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HighlightTextTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text myText; 
    public Color highlightColor = Color.black;  

    private Color originalColor;

    void Start()
    {
        if (myText == null)
        {
            myText = GetComponent<TMP_Text>();  
        }

        
        originalColor = myText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        myText.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        myText.color = originalColor;
    }
}

