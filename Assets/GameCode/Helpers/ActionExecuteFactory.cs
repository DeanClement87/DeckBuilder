using System;

public static class ActionExecuteFactory
{
    public static IActionExecute GetActionExecute(ActionEnum actionEnum)
    {
        switch (actionEnum)
        {
            case ActionEnum.Attack:
                return new AttackActionExecute();
            case ActionEnum.CleaveAttack:
                return new CleaveAttackActionExecute();
            case ActionEnum.Distract:
                return new DistractActionExecute();
            case ActionEnum.Jump:
                return new JumpActionExecute();
            case ActionEnum.DiscardRandomCard:
                return new DiscardRandomCardActionExecute();
            case ActionEnum.Mark:
                return new MarkActionExecute();
            case ActionEnum.Frozen:
                return new FrozenActionExecute();
            case ActionEnum.Stun:
                return new StunActionExecute();
            case ActionEnum.AddBlock:
                return new AddBlockActionExecute();
            case ActionEnum.AddThorns:
                return new AddThornsActionExecute();
            case ActionEnum.AddTownMood:
                return new AddTownMoodActionExecute();
             case ActionEnum.GiveManaToAllies:
                return new GiveManaToAlliesActionExecute("not_to_self");
            case ActionEnum.GiveManaToLane:
                return new GiveManaToAlliesActionExecute();
            case ActionEnum.GiveCardDrawToAllies:
                return new GiveCardDrawToAlliesActionExecute();
            case ActionEnum.BeAfraid:
                return new BeAfraidActionExecute();
            case ActionEnum.FeedOnFear:
                return new CleaveAttackActionExecute("injured");
            case ActionEnum.Frostbolt:
                return new FrostboltActionExecute();


            //AUTO CHECKERS
            case ActionEnum.SkipIfNoMonsterInLane:
                return new SkipIfNoMonsterInLaneActionExecute();
            case ActionEnum.SkipIfNoKill:
                return new SkipIfNoKillActionExecute();
            case ActionEnum.AlterNextActionValue:
                return new AlterNextActionValueExecute();
            default:
                throw new ArgumentOutOfRangeException(nameof(actionEnum), actionEnum, "Unsupported action");
        }
    }
}


