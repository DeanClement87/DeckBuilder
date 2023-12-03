using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameManager gameManager;
    private RectTransform rectTransform;
    private Vector3 startingPosition;

    public ContactFilter2D contactFilter;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroPlacement) return;

        startingPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroPlacement) return;

        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroPlacement) return;

        RaycastHit2D[] hits = new RaycastHit2D[1];

        if (Physics2D.Raycast(transform.position, Vector3.down, contactFilter, hits, 5) > 0)
        {
            var ls = hits[0].collider.GetComponent<LaneScript>();
            var hs = GetComponent<HeroScript>();

            var heroLane = gameManager.HeroLanes.First(x => x.laneNumber == ls.laneNumber);
            if (heroLane.HeroesModels.Count() >= 3)
            {
                transform.position = startingPosition;
            }
            else
            {
                gameManager.AddHeroToLane(eventData.pointerDrag, hs.HeroModel, ls.laneNumber);
            }
        }
        else
        {
            transform.position = startingPosition;
        }
    }
}