using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameObject MainCanvas { get; set; }
    
    //ENTITIES
    public List<HeroModel> Heroes { get; set; } = new List<HeroModel>();
    public List<MonsterModel> Monsters { get; set; } = new List<MonsterModel>();
    public TownModel Town { get; set; } = new TownModel();

    //LANES
    public List<LaneModel> HeroLanes { get; set; } = new List<LaneModel>();
    public List<LaneModel> MonsterLanes { get; set; } = new List<LaneModel>();

    //THE ACTIVE SELECTIONS
    public HeroModel ActiveHero { get; set; }
    public CardModel ActiveCard { get; set; }

    //THINGS THAT HAVE BEEN CLICKED
    public MonsterModel TargetedMonster { get; set; }
    public LaneModel TargetedLane { get; set; }
    public HeroModel TargetedHero { get; set; }

    //WHERE WE ARE UP TO
    public GameState gameState { get; set; }
    public int WaveCounter { get; set; }

    public enum GameState
    {
        GameStart,
        LevelStart,
        HeroSpawn,
        HeroPlacement,
        MonsterSpawn,
        MonsterTurn,
        HeroTurn,
        CardAction,
        EndRound,
        SelectCard
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();

                    _instance.ChangeGameState(GameState.GameStart);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeGameState(GameState gameState)
    {
        Debug.Log($"GAMESTATE CHANGE: {this.gameState} -> {gameState}");
        this.gameState = gameState;
        MainCanvas =  GameObject.Find("MainCanvas");

        //THIS IS TEMPORARY FOR DEBUGGING
        var gameObject = GameObject.Find("GameState");
        TextMeshProUGUI textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = gameState.ToString();
        //END TEMP

        switch (gameState)
        {
            case GameState.GameStart:
                //add lanes
                HeroLanes.Add(new LaneModel(1));
                HeroLanes.Add(new LaneModel(2));
                HeroLanes.Add(new LaneModel(3));
                MonsterLanes.Add(new LaneModel(1));
                MonsterLanes.Add(new LaneModel(2));
                MonsterLanes.Add(new LaneModel(3));

                for (int i = 0; i < 3; i++)
                {
                    HeroLanes[i].OppositeLane = MonsterLanes[i];
                    MonsterLanes[i].OppositeLane = HeroLanes[i];
                }

                //add 4 heroes
                Heroes.Add(new HeroModel());
                Heroes.Add(new HeroModel());
                Heroes.Add(new HeroModel());
                Heroes.Add(new HeroModel());

                //init the hero decks with cards etc
                InitHeroesScript.InitHeroKnightDeck(Heroes[0]);
                InitHeroesScript.InitHeroRangerDeck(Heroes[1]);
                InitHeroesScript.InitHeroWizardDeck(Heroes[2]);
                InitHeroesScript.InitHeroRogueDeck(Heroes[3]);

                HideGameObjectOffScreen("EndHeroTurn", true);
                HideGameObjectOffScreen("ConfirmHeroPlacement", true);

                ChangeGameState(GameState.LevelStart);
                break;
            case GameState.LevelStart:
                Town.Health = Town.BaseHealth;
                Town.Mood = 0;
                WaveCounter = 1;

                Monsters = InitMonsterWorld1.InitMonsters(1); //TODO: handle multiple worlds

                ChangeGameState(GameState.HeroSpawn);
                break;
            case GameState.HeroSpawn:
                //load the herotray into its correct position from off screen
                PositionHelper.ChangePositionY("HeroTray", -200);

                int heroOrder = 1;
                foreach (var hero in Heroes)
                {
                    HeroSpawn(hero, heroOrder);
                    heroOrder++;
                }

                ChangeGameState(GameState.HeroPlacement);
                break;
            case GameState.HeroPlacement:
                HideGameObjectOffScreen("ConfirmHeroPlacement", false);

                break;
            case GameState.MonsterSpawn:
                //if no more waves, dont try and spawn anymore monsters
                if (WaveCounter > 7)
                {
                    ChangeGameState(GameState.HeroTurn);
                    break;
                }

                //spawn in a monster for each lane
                foreach (var lane in MonsterLanes)
                {
                    int randomIndex = UnityEngine.Random.Range(0, Monsters.Count);

                    if (lane.MonsterModels.Count() == 3) //overflow
                    {
                        MonsterOverflow(Monsters[randomIndex]);
                    }
                    else
                    {
                        //create a monster object
                        var monsterPrefab = Resources.Load<GameObject>("Monster");
                        GameObject monsterObject = GameObject.Instantiate(monsterPrefab, Vector3.zero, Quaternion.identity);
                        monsterObject.transform.SetParent(MainCanvas.transform, true);

                        MonsterScript ms = monsterObject.GetComponent<MonsterScript>();
                        ms.SetMonsterData(Monsters[randomIndex]);

                        AddMonsterToLane(monsterObject, Monsters[randomIndex], lane.laneNumber);
                    }

                    Monsters.RemoveAt(randomIndex);
                }

                ChangeGameState(GameState.HeroTurn);
                break;
            case GameState.HeroTurn:
                HideGameObjectOffScreen("EndHeroTurn", false);

                foreach (var hero in Heroes)
                {
                    if (hero.Dead == false)
                    {
                        SetActiveHero(hero);
                        break;
                    }
                }

                foreach (var hero in Heroes)
                {
                    hero.Mana += 3;
                    if (hero.Mana > 5) hero.Mana = 5;
                    hero.DrawCard(4);
                    hero.Initiative = true;
                }

                break;
            case GameState.CardAction:
                var actionManager = ActionManager.Instance;
                actionManager.ActionCounter = 0;
                actionManager.ActionExecutor();

                break;
            case GameState.MonsterTurn:
                var monsterManager = MonsterManager.Instance;

                monsterManager.MonsterExecutorStart();

                break;

            case GameState.EndRound:
                WaveCounter++;
                //remove all hero temp buffs
                foreach (var hero in Heroes.Where(x => x.Dead == false))
                {
                    hero.Block = 0;
                    hero.Thorns = 0;
                    hero.BeAfraid = false;
                }

                //remove all monster debuffs
                foreach (var monsterLane in MonsterLanes)
                {
                    foreach (var monsterObject in monsterLane.ObjectsInLane)
                    {
                        var ms = monsterObject.GetComponent<MonsterScript>();
                        ms.ResetDebuffs();
                    }
                }

                //town takes mood damage
                if (Town.Hope() > 0)
                {
                    Town.Health += Town.Hope();
                }
                else if (Town.Fear() > 0)
                {
                    Town.Health -= Town.Fear();
                }

                //discard all heroes hands.
                foreach (var hero in Heroes)
                hero.DiscardHand();

                ChangeGameState(GameState.HeroPlacement);

                break;
            case GameState.SelectCard:
            
                break;
        }
    }

    public void MonsterOverflow(MonsterModel monsterModel)
    {
        //do something when monster overflows.
        //some sort of damage to town.
    }

    public void HideGameObjectOffScreen(string gameObjectName, bool hide)
    {
        var gameObject = GameObject.Find(gameObjectName);

        Vector3 pos = gameObject.transform.position;
        if (hide)
            pos.y -= 1000;
        else
            pos.y += 1000;

        gameObject.transform.position = pos;
    }

    public void HeroSpawn(HeroModel hero, int heroOrder)
    {
        var heroTray = GameObject.Find("HeroTray");
        var position = heroTray.transform.position;

        switch (heroOrder)
        {
            case 1:
                position.x -= 220;
                break;
            case 2:
                position.x -= 70;
                break;
            case 3:
                position.x += 70;
                break;
            case 4:
                position.x += 220;
                break;
        }

        //create an empty hero object.
        var heroPrefab = Resources.Load<GameObject>("Hero");
        GameObject newHero = GameObject.Instantiate(heroPrefab, position, Quaternion.identity);
        newHero.transform.SetParent(MainCanvas.transform, true);

        HeroScript hs = newHero.GetComponent<HeroScript>();
        hs.SetHeroData(hero);
    }

    public void AddHeroToLane(GameObject hero, HeroModel heroModel, int laneNumber)
    {
        foreach (var lane in HeroLanes)
        {
            lane.ObjectsInLane.Remove(hero);
            lane.HeroesModels.Remove(heroModel);
        }

        HeroLanes[laneNumber - 1].ObjectsInLane.Add(hero);
        HeroLanes[laneNumber - 1].HeroesModels.Add(heroModel);

        GameObject laneImage1 = GameObject.Find("HeroLaneImage1");
        GameObject laneImage2 = GameObject.Find("HeroLaneImage2");
        GameObject laneImage3 = GameObject.Find("HeroLaneImage3");

        Vector3 position = laneImage1.transform.position;

        //reposition heroes within the lane to looks neat
        for (int i = 0; i < 3; i++)
        {
            if (i == 0) position = laneImage1.transform.position;
            if (i == 1) position = laneImage2.transform.position;
            if (i == 2) position = laneImage3.transform.position;

            if (HeroLanes[i].ObjectsInLane.Count() == 1)
            {
                HeroLanes[i].ObjectsInLane[0].transform.position = position;
                continue;
            }

            if (HeroLanes[i].ObjectsInLane.Count() == 2)
            {
                position.x = position.x - 100;
                HeroLanes[i].ObjectsInLane[0].transform.position = position;

                position.x = position.x + 200;
                HeroLanes[i].ObjectsInLane[1].transform.position = position;
                continue;
            }

            if (HeroLanes[i].ObjectsInLane.Count() == 3)
            {
                position.x = position.x - 150;
                HeroLanes[i].ObjectsInLane[0].transform.position = position;

                position.x = position.x + 150;
                HeroLanes[i].ObjectsInLane[1].transform.position = position;

                position.x = position.x + 150;
                HeroLanes[i].ObjectsInLane[2].transform.position = position;
                continue;
            }
        }
    }

    public void AddMonsterToLane(GameObject monster, MonsterModel monsterModel, int laneNumber)
    {
        AddRemoveMonsterToLane(monster, monsterModel, laneNumber, true);
    }

    public void RemoveMonsterFromGame(GameObject monster, MonsterModel monsterModel)
    {
        AddRemoveMonsterToLane(monster, monsterModel, 0, false);
    }

    private void AddRemoveMonsterToLane(GameObject monster, MonsterModel monsterModel, int laneNumber, bool Add)
    {
        foreach (var lane in MonsterLanes)
        {
            lane.ObjectsInLane.Remove(monster);
            lane.MonsterModels.Remove(monsterModel);
        }

        if (Add)
        {
            MonsterLanes[laneNumber - 1].ObjectsInLane.Add(monster);
            MonsterLanes[laneNumber - 1].MonsterModels.Add(monsterModel);
        }
        else
        {
            Destroy(monster);
        }


        GameObject laneImage1 = GameObject.Find("MonsterLaneImage1");
        GameObject laneImage2 = GameObject.Find("MonsterLaneImage2");
        GameObject laneImage3 = GameObject.Find("MonsterLaneImage3");

        Vector3 position = laneImage1.transform.position;

        //reposition monsters within the lane to looks neat
        for (int i = 0; i < 3; i++)
        {
            if (i == 0) position = laneImage1.transform.position;
            else if (i == 1) position = laneImage2.transform.position;
            else if (i == 2) position = laneImage3.transform.position;

            if (MonsterLanes[i].ObjectsInLane.Count() == 1)
            {
                MonsterLanes[i].ObjectsInLane[0].transform.position = position;
                continue;
            }

            if (MonsterLanes[i].ObjectsInLane.Count() == 2)
            {
                position.x = position.x - 100;
                MonsterLanes[i].ObjectsInLane[0].transform.position = position;

                position.x = position.x + 200;
                MonsterLanes[i].ObjectsInLane[1].transform.position = position;
                continue;
            }

            if (MonsterLanes[i].ObjectsInLane.Count() == 3)
            {
                position.x = position.x - 175;
                MonsterLanes[i].ObjectsInLane[0].transform.position = position;

                position.x = position.x + 175;
                MonsterLanes[i].ObjectsInLane[1].transform.position = position;

                position.x = position.x + 175;
                MonsterLanes[i].ObjectsInLane[2].transform.position = position;
                continue;
            }
        }
    }

    public void SetActiveHero(HeroModel heroModel)
    {
        var activeY = -390;
        var inactiveY = -700;

        if (ActiveHero == heroModel)
            return;

        //hide old active heros cards.
        if (ActiveHero != null)
        {
            foreach (var cardGameObject in ActiveHero.Hand)
            {
                PositionHelper.ChangePositionY(cardGameObject, inactiveY);
            }
        }

        ActiveHero = heroModel;

        //show new active heros cards.
        foreach (var cardGameObject in ActiveHero.Hand)
        {
            PositionHelper.ChangePositionY(cardGameObject, activeY);
        }

        var black = Resources.Load<Sprite>("AvatarAssets/BlackAvatarBackground");
        var blue = Resources.Load<Sprite>("AvatarAssets/BlueAvatarBackground");

        for (int i = 1; i <= 4; i++)
        {
            var avatar = GameObject.Find($"Avatar{i}");
            var avatarScript = avatar.GetComponentInChildren<AvatarScript>();
            var avatarImage = avatar.GetComponent<Image>();
            if (avatarScript.GetHeroModel() == ActiveHero)
                avatarImage.sprite = blue;
            else
                avatarImage.sprite = black;
        }
            
    }
}
