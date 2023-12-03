using System;

public static class CardConditionFactory
{
    public static ICardConditionCheck GetCardConditionCheck(CardConditionEnum cardConditionEnum)
    {
        switch (cardConditionEnum)
        {
            case CardConditionEnum.FriendlyInLane:
                return new FriendlyInLaneCheck();
            case CardConditionEnum.MonsterInThisLane:
                return new MonsterInThisLaneCheck();
            case CardConditionEnum.MonsterOutsideLane:
                return new MonsterOutsideLaneCheck();
            case CardConditionEnum.MonsterAnyLane:
                return new MonsterAnyLaneCheck();
            default:
                throw new ArgumentOutOfRangeException(nameof(cardConditionEnum), cardConditionEnum, "Unsupported card condition");
        }
    }
}


