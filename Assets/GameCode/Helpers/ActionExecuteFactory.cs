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
            case ActionEnum.Mark:
                return new MarkActionExecute();
            case ActionEnum.Stun:
                return new StunActionExecute();
            case ActionEnum.AddBlock:
                return new AddBlockActionExecute();
            case ActionEnum.AddThorns:
                return new AddThornsActionExecute();
            case ActionEnum.AddTownMood:
                return new AddTownMoodActionExecute();
            case ActionEnum.AttackByThornsStack:
                return new AttackByThornsStackActionExecute();
            case ActionEnum.AttackPlusFear:
                return new AttackPlusFearActionExecute();
            case ActionEnum.GiveManaToAllies:
                return new GiveManaToAlliesActionExecute("not_to_self");
            case ActionEnum.GiveManaToLane:
                return new GiveManaToAlliesActionExecute();
            case ActionEnum.GiveCardDrawToAllies:
                return new GiveCardDrawToAlliesActionExecute();
            case ActionEnum.BeAfraid:
                return new BeAfraidActionExecute();
            case ActionEnum.Backstab:
                return new BackstabActionExecute();



            //AUTO CHECKERS
            case ActionEnum.SkipIfNoMonsterInLane:
                return new SkipIfNoMonsterInLaneActionExecute();
            case ActionEnum.SkipIfNoKill:
                return new SkipIfNoKillActionExecute();
            default:
                throw new ArgumentOutOfRangeException(nameof(actionEnum), actionEnum, "Unsupported action");
        }
    }
}


