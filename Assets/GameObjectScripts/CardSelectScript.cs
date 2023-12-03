using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectScript : MonoBehaviour
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

    private void OnMouseDown()
    {
        //add this card to hero
        gameManager.ActiveHero.Deck.Add(cardModel);

        if (gameManager.ActiveHero.Hand.Count() < 6)
        {
            var heroCardPrefab = Resources.Load<GameObject>("HeroCard");
            GameObject newCard = GameObject.Instantiate(heroCardPrefab, Vector3.zero, Quaternion.identity);
            newCard.transform.SetParent(gameManager.MainCanvas.transform, true);
            gameManager.ActiveHero.Hand.Add(newCard);
            gameManager.ActiveHero.OrganiseHand();

            CardScript c = newCard.GetComponent<CardScript>();
            c.SetCardData(cardModel, gameManager.ActiveHero);

            
        }
        else
        {
            gameManager.ActiveHero.DiscardPile.Add(cardModel);
        }

        //remove card options
        var objectsWithTag = GameObject.FindGameObjectsWithTag("CardSelectTag");
        foreach (GameObject obj in objectsWithTag)
            Destroy(obj);

        gameManager.gameState = GameManager.GameState.HeroTurn;
    }
}
