using System.Linq;

public class MonsterAnyLaneCheck : ICardConditionCheck
{
    private GameManager gameManager;

    public MonsterAnyLaneCheck()
    {
        gameManager = GameManager.Instance;
    }

    public bool ConditionMet()
    {
        foreach (var monsterLane in gameManager.MonsterLanes)
        {
            if (monsterLane.MonsterModels.Any()) return true;
        }

        return false;
    }
}
