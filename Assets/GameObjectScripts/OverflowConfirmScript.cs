using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OverflowConfirmScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var overflowScreen = GameObject.Find($"OverflowScreen");
        var s = overflowScreen.GetComponent<OverFlowScreenScript>();
        s.ConfirmOverflow();

    }
}
