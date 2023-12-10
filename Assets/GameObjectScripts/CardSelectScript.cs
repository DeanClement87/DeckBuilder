using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelectScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    private CardModel cardModel;
    private HeroModel heroModel;

    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public TMP_Text manaCost;
    public Image cardImage;

    private Image cardTemplate;

    void Awake()
    {
        gameManager = GameManager.Instance;
        cardTemplate = GetComponent<Image>();
    }

    public void SetCardData(CardModel card, HeroModel hero)
    {
        cardModel = card;
        heroModel = hero;

        cardName.text = card.BaseCard.CardName;
        cardDescription.text = card.BaseCard.Description;
        manaCost.text = card.BaseCard.ManaCost.ToString();

        Sprite cardSprite = Resources.Load<Sprite>(card.BaseCard.Image);
        cardImage.sprite = cardSprite;

        Sprite cardTemplateSprite = Resources.Load<Sprite>(hero.CardTemplateArt);
        cardTemplate.sprite = cardTemplateSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //add this card to hero
        heroModel.Deck.Add(cardModel);

        if (heroModel.Hand.Count() < 6)
        {
            var heroCardPrefab = Resources.Load<GameObject>("HeroCard");
            GameObject newCard = GameObject.Instantiate(heroCardPrefab, Vector3.zero, Quaternion.identity);
            newCard.transform.SetParent(gameManager.MainCanvas.transform, true);
            heroModel.Hand.Add(newCard);
            heroModel.OrganiseHand();

            CardScript c = newCard.GetComponent<CardScript>();
            c.SetCardData(cardModel, heroModel);
        }
        else
        {
            heroModel.DiscardPile.Add(cardModel);
        }

        if (gameManager.gameState == GameManager.GameState.SelectCardHeroTurn)
            gameManager.gameState = GameManager.GameState.HeroTurn;
        else if (gameManager.gameState == GameManager.GameState.SelectCardMonsterTurn)
            gameManager.gameState = GameManager.GameState.MonsterTurn;

        //remove card options
        var objectsWithTag = GameObject.FindGameObjectsWithTag("CardSelectTag");
        foreach (GameObject obj in objectsWithTag)
            Destroy(obj);
    }
}
