using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScaleOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    private void Start()
    {
        // Store the original scale
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up when the mouse is over
        transform.localScale = originalScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to the original scale when the mouse exits
        transform.localScale = originalScale;
    }
}