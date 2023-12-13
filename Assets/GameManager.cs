using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameObject MainCanvas { get; set; }
    public GameObject ConfirmHeroPlacement { get; set; }
    public GameObject EndHeroTurn { get; set; }
    
    //ENTITIES
    public List<HeroEnum> HeroesSelectionFromMainScreen { get; set; } = new List<HeroEnum>();
    public List<HeroModel> Heroes { get; set; } = new List<HeroModel>();
    public List<MonsterModel> Monsters { get; set; } = new List<MonsterModel>();
    public TownModel Town { get; set; } = new TownModel();

    //LANES
    public List<LaneModel> HeroLanes { get; set; } = new List<LaneModel>();
    public List<LaneModel> MonsterLanes { get; set; } = new List<LaneModel>();

    //THE ACTIVE SELECTIONS
    public HeroModel ActiveHero { get; set; }
    public GameObject ActiveHeroObject { get; set; }
    public CardModel ActiveCard { get; set; }

    //THINGS THAT HAVE BEEN CLICKED
    public MonsterModel TargetedMonster { get; set; }
    public GameObject TargetedMonsterObject { get; set; }
    public LaneModel TargetedLane { get; set; }
    public HeroModel TargetedHero { get; set; }

    //WHERE WE ARE UP TO
    public int world { get; set; }
    public int level { get; set; }
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
        SelectCardHeroTurn,
        SelectCardMonsterTurn,
        MonsterOverflow,
        GameOver,
        LevelComplete
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
        MainCanvas = GameObject.Find("MainCanvas");

        //THIS IS TEMPORARY FOR DEBUGGING
        var gameObject = GameObject.Find("GameState");
        TextMeshProUGUI textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = gameState.ToString();
        //END TEMP

        switch (gameState)
        {
            case GameState.GameStart:
                world = 1;
                level = 1;

                Heroes = new List<HeroModel>();

                if (HeroesSelectionFromMainScreen.Any())
                {
                    //add 4 heroes
                    Heroes.Add(CreateHero(HeroesSelectionFromMainScreen[0], 1));
                    Heroes.Add(CreateHero(HeroesSelectionFromMainScreen[1], 2));
                    Heroes.Add(CreateHero(HeroesSelectionFromMainScreen[2], 3));
                    Heroes.Add(CreateHero(HeroesSelectionFromMainScreen[3], 4));
                    HeroesSelectionFromMainScreen = new List<HeroEnum>();
                }
                else
                {
                    //add 4 default heroes
                    Heroes.Add(CreateHero(HeroEnum.Knight, 1));
                    Heroes.Add(CreateHero(HeroEnum.Ranger, 2));
                    Heroes.Add(CreateHero(HeroEnum.Wizard, 3));
                    Heroes.Add(CreateHero(HeroEnum.Warlock, 4));
                }

                //set the avatars when selecting a card
                for (int i = 0; i < 4; i++)
                {
                    var cardSelectAvatar1 = GameObject.Find($"CardSelectAvatar{i+1}");
                    var csa = cardSelectAvatar1.GetComponent<CardSelectAvatarScript>();
                    csa.HeroModel = Heroes[i];
                }

                ConfirmHeroPlacement = GameObject.Find("ConfirmHeroPlacement");
                ConfirmHeroPlacement.SetActive(false);

                EndHeroTurn = GameObject.Find("EndHeroTurn");
                EndHeroTurn.SetActive(false);

                ChangeGameState(GameState.LevelStart);
                break;
            case GameState.LevelStart:
                Town.Health = Town.BaseHealth;
                Town.Mood = 0;
                WaveCounter = 1;

                //reset heroes cards
                //reset heroes health
                foreach (var hero in Heroes)
                {
                    hero.Health = hero.BaseHealth;
                    hero.ResetHeroesCards();
                }

                //remove any objects left in lanes
                foreach (var lane in HeroLanes)
                {
                    foreach (var obj in lane.ObjectsInLane)
                        Destroy(obj);
                }

                foreach (var lane in MonsterLanes)
                {
                    foreach (var obj in lane.ObjectsInLane)
                        Destroy(obj);
                }

                //reset lanes
                HeroLanes = new List<LaneModel>();
                MonsterLanes = new List<LaneModel>();

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
                ConfirmHeroPlacement.SetActive(true);

                break;
            case GameState.MonsterSpawn:
                //if no more monsters, dont try and spawn anymore monsters
                if (Monsters.Any() == false)
                {
                    ChangeGameState(GameState.HeroTurn);
                    break;
                }

                //spawn in a monster for each lane
                var monsterOverflow = new List<MonsterModel>();
                foreach (var lane in MonsterLanes)
                {
                    if (!Monsters.Any()) continue;

                    int randomIndex = UnityEngine.Random.Range(0, Monsters.Count());

                    if (lane.MonsterModels.Count() == 3) //overflow
                    {
                        monsterOverflow.Add(Monsters[randomIndex]);
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
                        Monsters.RemoveAt(randomIndex);
                    }         
                }

                HandleMonsterOverflowQueue(monsterOverflow);

                
                break;
            case GameState.HeroTurn:
                EndHeroTurn.SetActive(true);

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
            case GameState.GameOver:
                var gameOverScreen = GameObject.Find($"GameOverScreen");
                gameOverScreen.transform.localPosition = Vector3.zero;
                gameOverScreen.transform.SetAsLastSibling();

                var s = gameOverScreen.GetComponent<GameOverScreenScript>();
                s.SetGameOverStats();

                break;
            case GameState.LevelComplete:
                var levelCompleteScreen = GameObject.Find($"LevelCompleteScreen");
                levelCompleteScreen.transform.localPosition = Vector3.zero;
                levelCompleteScreen.transform.SetAsLastSibling();

                var gos = levelCompleteScreen.GetComponent<LevelCompleteScreenScript>();
                gos.SetLevelCompleteStats();

                break;
        }
    }

    public HeroModel CreateHero(HeroEnum heroEnum, int avatarNumber)
    {
        var newHero = new HeroModel();
        switch (heroEnum)
        {
            case HeroEnum.Knight:
                InitHeroesScript.InitHeroKnightDeck(newHero);
                break;
            case HeroEnum.Ranger:
                InitHeroesScript.InitHeroRangerDeck(newHero);
                break;
            case HeroEnum.Warlock:
                InitHeroesScript.InitHeroWarlockDeck(newHero);
                break;
            case HeroEnum.Rogue:
                InitHeroesScript.InitHeroRogueDeck(newHero);
                break;
            case HeroEnum.Wizard:
                InitHeroesScript.InitHeroWizardDeck(newHero);
                break;
        }

        var avatar = GameObject.Find($"HeroSelectImage{avatarNumber}");
        var avatarScript = avatar.GetComponent<AvatarScript>();
        avatarScript.HeroModel = newHero;

        return newHero;
    }

    public void HandleMonsterOverflowQueue(List<MonsterModel> monsterOverflowQueue)
    {
        if (monsterOverflowQueue.Any() == false)
        {
            ChangeGameState(GameState.HeroTurn);
            return;
        }

        ChangeGameState(GameState.MonsterOverflow);

        var overflowScreen = GameObject.Find($"OverflowScreen");
        overflowScreen.transform.localPosition = Vector3.zero;
        overflowScreen.transform.SetAsLastSibling();

        var s = overflowScreen.GetComponent<OverFlowScreenScript>();
        s.StartOverflow(monsterOverflowQueue);
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
        var monstersInLanesCount = 0;
        foreach (var lane in MonsterLanes)
        {
            lane.ObjectsInLane.Remove(monster);
            lane.MonsterModels.Remove(monsterModel);
            monstersInLanesCount += lane.MonsterModels.Count();
        }

        if (Add)
        {
            MonsterLanes[laneNumber - 1].ObjectsInLane.Add(monster);
            MonsterLanes[laneNumber - 1].MonsterModels.Add(monsterModel);
        }
        else
        {
            Destroy(monster);

            //check if there are any monsters left.
            //if none then level complete
            if (!Monsters.Any() && monstersInLanesCount == 0)
            {
                ChangeGameState(GameState.LevelComplete);
                return;
            }

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

        foreach (var lane in HeroLanes)
        {
            foreach (var heroObject in lane.ObjectsInLane)
            {
                var hs = heroObject.GetComponent<HeroScript>();
                if (hs.HeroModel == heroModel)
                {
                    ActiveHeroObject = heroObject;
                }
            }
        }

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
            if (avatarScript.HeroModel == ActiveHero)
                avatarImage.sprite = blue;
            else
                avatarImage.sprite = black;
        }
            
    }
}
