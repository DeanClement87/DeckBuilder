using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
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

    public CardModel GetCardModel()
    {
        return cardModel;
    }

    public void ActivateCard()
    {
        Debug.Log("ACTIVATED CARD " + cardModel.BaseCard.CardName);

        gameManager.ActiveHero.Mana -= cardModel.BaseCard.ManaCost; //needs to change to card mana not base card mana

        gameManager.ActiveCard = cardModel;
        gameManager.ChangeGameState(GameManager.GameState.CardAction);


        //remove card.
        gameManager.ActiveHero.DiscardPile.Add(cardModel);
        gameManager.ActiveHero.Hand.Remove(gameObject);
        gameManager.ActiveHero.OrganiseHand();
        Destroy(gameObject);
    }
}
