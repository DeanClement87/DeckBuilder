using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroModel : ScriptableObject
{
    private GameManager gameManager;
    //CARDS
    public List<BaseCardModel> PossibleCards { get; set; } = new List<BaseCardModel>();
    public List<CardModel> Deck { get; set; } = new List<CardModel>();
    public List<CardModel> DrawPile { get; set; } = new List<CardModel>();
    public List<CardModel> DiscardPile { get; set; } = new List<CardModel>();
    public List<GameObject> Hand { get; set; } = new List<GameObject>();
    //ART
    public string HeroArt { get; set; }
    public string HeroBoardArt { get; set; }
    public string HeroArtAvatar { get; set; }
    public string CardTemplateArt { get; set; }
    //STATS
    public int BaseHealth { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    public bool Dead { get; set; }
    //BUFFS
    public int Block { get; set; }
    public int Thorns { get; set; }
    public bool BeAfraid { get; set; }
    public bool Initiative { get; set; }

    public HeroModel()
    {
        gameManager = GameManager.Instance;
    }
    public void OrganiseHand()
    {
        int cardCount = 0;
        foreach (var gameCard in Hand)
        {
            var y = -390;
            if (gameManager.ActiveHero != this)
                y = -700;

            PositionHelper.ChangePositionXY(gameCard, -550 + (210 * cardCount), y);

            cardCount++;
        }
    }

    public void DiscardHand()
    {
        foreach (var card in Hand)
        {
            CardScript c = card.GetComponent<CardScript>();
            DiscardPile.Add(c.GetCardModel());

            Destroy(card);
        }

        Hand = new List<GameObject>();
    }

    public void DiscardRandomCard()
    {
        int randomNumber = UnityEngine.Random.Range(0, Hand.Count());

        DiscardCard(Hand[randomNumber]);
    }

    public void DiscardCard(GameObject cardToDiscard)
    {
        Hand.Remove(cardToDiscard);

        var c = cardToDiscard.GetComponent<CardScript>();
        DiscardPile.Add(c.GetCardModel());

        Destroy(cardToDiscard);

        OrganiseHand();
    }

    public void ResetHeroesCards()
    {
        foreach (var card in Hand)
        {
            DiscardCard(card);
        }

        DrawPile.AddRange(Deck);
        DiscardPile = new List<CardModel>();
    }

    public void Shuffle()
    {
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
    }

    public void DrawCard(int numberOfCards = 1)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            // Max cards
            if (Hand.Count() >= 6)
                return;

            // Draw pile is empty, shuffle before drawing.
            if (!DrawPile.Any())
                Shuffle();

            // Create an empty card object and add it to the hero's hand
            var heroCardPrefab = Resources.Load<GameObject>("HeroCard");
            GameObject newCard = GameObject.Instantiate(heroCardPrefab, Vector3.zero, Quaternion.identity);
            newCard.transform.SetParent(gameManager.MainCanvas.transform, true);

            Hand.Add(newCard);
            OrganiseHand();

            // Randomly select a card from the draw pile and add it to the card object we just created
            int randomNumber = UnityEngine.Random.Range(0, DrawPile.Count());
            CardScript c = newCard.GetComponent<CardScript>();
            c.SetCardData(DrawPile[randomNumber], this);

            DrawPile.RemoveAt(randomNumber);
        }
    }

    public void ShowCardOptions()
    {
        var cardRewardScreen = GameObject.Find("CardRewardScreen");

        int[] cardIndexesOffered = { -1, -1, -1 };

        for (int i = 0; i < 3; i++)
        {
            //create an empty card object
            var vector = new Vector3(-330 + (330 * i), -290, 0);
            var heroCardChoicePrefab = Resources.Load<GameObject>("HeroCardChoice");
            GameObject newCard = GameObject.Instantiate(heroCardChoicePrefab, vector, Quaternion.identity);
            newCard.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            newCard.transform.SetParent(cardRewardScreen.transform, false);

            //randomly select a card from the possible cards for this hero
            int cardIndexToOffer = -1;
            while (cardIndexToOffer == -1)
            {
                var randomNumber = UnityEngine.Random.Range(0, PossibleCards.Count());
                if (!cardIndexesOffered.Contains(randomNumber))
                {
                    cardIndexToOffer = randomNumber;
                    cardIndexesOffered[i] = randomNumber;
                }
            }

            var script = newCard.GetComponent<CardSelectScript>();
            var cardModel = new CardModel(PossibleCards[cardIndexToOffer]);
            script.SetCardData(cardModel, this);
        }
    }

    public void HeroDeath()
    {
        var heroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(this));
        foreach (var hero in heroLane.ObjectsInLane)
        {
            var hs = hero.GetComponent<HeroScript>();
            if (hs.HeroModel == this)
            {
                Dead = true;
                heroLane.HeroesModels.Remove(this);
                heroLane.ObjectsInLane.Remove(hero);
                Destroy(hero);

                //if all heroes are dead
                if (gameManager.Heroes.Any(x => x.Dead == false) == false)
                {
                    gameManager.ChangeGameState(GameManager.GameState.GameOver);
                }
                break;
            }    
        }
    }
}
