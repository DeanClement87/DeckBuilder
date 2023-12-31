using UnityEngine;

public class InitHeroesScript
{
    public static void InitHeroKnightDeck(HeroModel hero)
    {
        hero.heroEnum = HeroEnum.Knight;
        hero.HeroArt = "HeroArt/Knight1";
        hero.HeroBoardArt = "HeroArt/Knight1Board";
        hero.HeroArtAvatar = "HeroArt/Knight1Avatar";
        hero.CardTemplateArt = "CardAssets/KnightCard";
        hero.BaseHealth = 20;

        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Knight/Retaliate"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Knight/Defend"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Knight/StrikeBack"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Knight/Rally"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Knight/ShieldsUp"));

        var card1 = new CardModel(hero.PossibleCards[0]); //Knight_Retaliate
        var card2 = new CardModel(hero.PossibleCards[0]); //Knight_Retaliate
        var card3 = new CardModel(hero.PossibleCards[0]); //Knight_Retaliate
        var card4 = new CardModel(hero.PossibleCards[0]); //Knight_Retaliate
        var card5 = new CardModel(hero.PossibleCards[1]); //Knight_Defend
        var card6 = new CardModel(hero.PossibleCards[1]); //Knight_Defend
        var card7 = new CardModel(hero.PossibleCards[1]); //Knight_Defend
        var card8 = new CardModel(hero.PossibleCards[1]); //Knight_Defend
        var card9 = new CardModel(hero.PossibleCards[2]); //Knight_StrikeBack
        var card10 = new CardModel(hero.PossibleCards[3]); //Knight_Rally

        hero.Deck.Add(card1);
        hero.Deck.Add(card2);
        hero.Deck.Add(card3);
        hero.Deck.Add(card4);
        hero.Deck.Add(card5);
        hero.Deck.Add(card6);
        hero.Deck.Add(card7);
        hero.Deck.Add(card8);
        hero.Deck.Add(card9);
        hero.Deck.Add(card10);

        //draw pile starts as full deck.
        hero.DrawPile.AddRange(hero.Deck);
    }

    public static void InitHeroRangerDeck(HeroModel hero)
    {
        hero.heroEnum = HeroEnum.Ranger;
        hero.HeroArt = "HeroArt/Ranger1";
        hero.HeroBoardArt = "HeroArt/Ranger1Board";
        hero.HeroArtAvatar = "HeroArt/Ranger1Avatar";
        hero.CardTemplateArt = "CardAssets/RangerCard";
        hero.BaseHealth = 15;

        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Ranger/Shoot"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Ranger/Diversion"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Ranger/EnemySpotted"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Ranger/Snipe"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Ranger/TripleShot"));


        var card1 = new CardModel(hero.PossibleCards[0]); //Ranger_Shoot
        var card2 = new CardModel(hero.PossibleCards[0]); //Ranger_Shoot
        var card3 = new CardModel(hero.PossibleCards[0]); //Ranger_Shoot
        var card4 = new CardModel(hero.PossibleCards[0]); //Ranger_Shoot
        var card5 = new CardModel(hero.PossibleCards[1]); //Ranger_Diversion
        var card6 = new CardModel(hero.PossibleCards[1]); //Ranger_Diversion
        var card7 = new CardModel(hero.PossibleCards[1]); //Ranger_Diversion
        var card8 = new CardModel(hero.PossibleCards[1]); //Ranger_Diversion
        var card9 = new CardModel(hero.PossibleCards[2]); //Ranger_EnemySpotted
        var card10 = new CardModel(hero.PossibleCards[3]); //Ranger_Snipe

        hero.Deck.Add(card1);
        hero.Deck.Add(card2);
        hero.Deck.Add(card3);
        hero.Deck.Add(card4);
        hero.Deck.Add(card5);
        hero.Deck.Add(card6);
        hero.Deck.Add(card7);
        hero.Deck.Add(card8);
        hero.Deck.Add(card9);
        hero.Deck.Add(card10);

        //draw pile starts as full deck.
        hero.DrawPile.AddRange(hero.Deck);
    }

    public static void InitHeroWarlockDeck(HeroModel hero)
    {
        hero.heroEnum = HeroEnum.Warlock;
        hero.HeroArt = "HeroArt/Warlock1";
        hero.HeroBoardArt = "HeroArt/Warlock1Board";
        hero.HeroArtAvatar = "HeroArt/Warlock1Avatar";
        hero.CardTemplateArt = "CardAssets/WarlockCard";
        hero.BaseHealth = 24;

        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Warlock/ChaosBolt"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Warlock/Nightmare"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Warlock/BeAfraid"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Warlock/SendThemToHell"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Warlock/FeedOnFear"));

        var card1 = new CardModel(hero.PossibleCards[0]); //Warlock_ChaosBolt
        var card2 = new CardModel(hero.PossibleCards[0]); //Warlock_ChaosBolt
        var card3 = new CardModel(hero.PossibleCards[0]); //Warlock_ChaosBolt
        var card4 = new CardModel(hero.PossibleCards[0]); //Warlock_ChaosBolt
        var card5 = new CardModel(hero.PossibleCards[1]); //Warlock_Nightmare
        var card6 = new CardModel(hero.PossibleCards[1]); //Warlock_Nightmare
        var card7 = new CardModel(hero.PossibleCards[1]); //Warlock_Nightmare
        var card8 = new CardModel(hero.PossibleCards[1]); //Warlock_Nightmare
        var card9 = new CardModel(hero.PossibleCards[2]); //Warlock_BeAfraid
        var card10 = new CardModel(hero.PossibleCards[3]); //Warlock_SendThemToHell

        hero.Deck.Add(card1);
        hero.Deck.Add(card2);
        hero.Deck.Add(card3);
        hero.Deck.Add(card4);
        hero.Deck.Add(card5);
        hero.Deck.Add(card6);
        hero.Deck.Add(card7);
        hero.Deck.Add(card8);
        hero.Deck.Add(card9);
        hero.Deck.Add(card10);

        //draw pile starts as full deck.
        hero.DrawPile.AddRange(hero.Deck);
    }

    public static void InitHeroRogueDeck(HeroModel hero)
    {
        hero.heroEnum = HeroEnum.Rogue;
        hero.HeroArt = "HeroArt/Rogue1";
        hero.HeroBoardArt = "HeroArt/Rogue1Board";
        hero.HeroArtAvatar = "HeroArt/Rogue1Avatar";
        hero.CardTemplateArt = "CardAssets/RogueCard";
        hero.BaseHealth = 13;

        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Rogue/Backstab"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Rogue/NowYouSeeMe"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Rogue/DualWield"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Rogue/Flanked"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Rogue/SmokeBomb"));

        var card1 = new CardModel(hero.PossibleCards[0]); //Rogue_Backstab
        var card2 = new CardModel(hero.PossibleCards[0]); //Rogue_Backstab
        var card3 = new CardModel(hero.PossibleCards[0]); //Rogue_Backstab
        var card4 = new CardModel(hero.PossibleCards[0]); //Rogue_Backstab
        var card5 = new CardModel(hero.PossibleCards[1]); //Rogue_NowYouSeeMe
        var card6 = new CardModel(hero.PossibleCards[1]); //Rogue_NowYouSeeMe
        var card7 = new CardModel(hero.PossibleCards[1]); //Rogue_NowYouSeeMe
        var card8 = new CardModel(hero.PossibleCards[1]); //Rogue_NowYouSeeMe
        var card9 = new CardModel(hero.PossibleCards[2]); //Rogue_DualWield
        var card10 = new CardModel(hero.PossibleCards[3]); //Rogue_Flanked

        hero.Deck.Add(card1);
        hero.Deck.Add(card2);
        hero.Deck.Add(card3);
        hero.Deck.Add(card4);
        hero.Deck.Add(card5);
        hero.Deck.Add(card6);
        hero.Deck.Add(card7);
        hero.Deck.Add(card8);
        hero.Deck.Add(card9);
        hero.Deck.Add(card10);

        //draw pile starts as full deck.
        hero.DrawPile.AddRange(hero.Deck);
    }

    public static void InitHeroWizardDeck(HeroModel hero)
    {
        hero.heroEnum = HeroEnum.Wizard;
        hero.HeroArt = "HeroArt/Wizard1";
        hero.HeroBoardArt = "HeroArt/Wizard1Board";
        hero.HeroArtAvatar = "HeroArt/Wizard1Avatar";
        hero.CardTemplateArt = "CardAssets/WizardCard";
        hero.BaseHealth = 13;

        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Wizard/Teleport"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Wizard/IceCage"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Wizard/Fireball"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Wizard/ManaInABottle"));
        hero.PossibleCards.Add(Resources.Load<BaseCardModel>("CardBases/Wizard/Frostbolt"));


        var card1 = new CardModel(hero.PossibleCards[0]); //Wizard_Teleport
        var card2 = new CardModel(hero.PossibleCards[0]); //Wizard_Teleport
        var card3 = new CardModel(hero.PossibleCards[0]); //Wizard_Teleport
        var card4 = new CardModel(hero.PossibleCards[0]); //Wizard_Teleport
        var card5 = new CardModel(hero.PossibleCards[1]); //Wizard_IceCage
        var card6 = new CardModel(hero.PossibleCards[1]); //Wizard_IceCage
        var card7 = new CardModel(hero.PossibleCards[1]); //Wizard_IceCage
        var card8 = new CardModel(hero.PossibleCards[1]); //Wizard_IceCage
        var card9 = new CardModel(hero.PossibleCards[2]); //Wizard_Fireball
        var card10 = new CardModel(hero.PossibleCards[3]);//Wizard_ManaInABottle

        hero.Deck.Add(card1);
        hero.Deck.Add(card2);
        hero.Deck.Add(card3);
        hero.Deck.Add(card4);
        hero.Deck.Add(card5);
        hero.Deck.Add(card6);
        hero.Deck.Add(card7);
        hero.Deck.Add(card8);
        hero.Deck.Add(card9);
        hero.Deck.Add(card10);

        //draw pile starts as full deck.
        hero.DrawPile.AddRange(hero.Deck);
    }
}
