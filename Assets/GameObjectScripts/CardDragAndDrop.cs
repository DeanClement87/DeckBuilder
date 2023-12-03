using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameManager gameManager;

    private RectTransform rectTransform;
    private Vector3 startingPosition;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroTurn) return;

        startingPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroTurn) return;

        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroTurn) return;

        var cs = GetComponentInParent<CardScript>();
        //card needs to go past the 480 y point to be considered played.
        //also hero needs to be able to afford to play the card
        //otherwise go back to where it started.
        if (eventData.position.y < 480 ||
            gameManager.ActiveHero.Mana - cs.GetCardModel().BaseCard.ManaCost < 0)
        {
            transform.position = startingPosition;
            return;
        }

        foreach (var conditionEnum in cs.GetCardModel().BaseCard.CardConditionEnums)
        {
            var cardConditionCheck = CardConditionFactory.GetCardConditionCheck(conditionEnum);

            if (cardConditionCheck.ConditionMet() == false)
            {
                transform.position = startingPosition;
                return;
            }
        }

        cs.ActivateCard();
    }
}
