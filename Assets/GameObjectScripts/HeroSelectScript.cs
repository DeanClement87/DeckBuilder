using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelectScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    private bool heroSelected;
    private Image avatarBorder;

    public HeroEnum hero;

    void Awake()
    {
        gameManager = GameManager.Instance;
        avatarBorder = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //hero was selected, so now we assume they unselected by pressing again
        if (heroSelected)
        {
            gameManager.HeroesSelectionFromMainScreen.Remove(hero);
            heroSelected = false;
        }
        else
        {
            gameManager.HeroesSelectionFromMainScreen.Add(hero);
            heroSelected = true;
        }

        if (heroSelected == true)
        {
            var blue = Resources.Load<Sprite>("AvatarAssets/BlueAvatarBackground");
            avatarBorder.sprite = blue;
        }
        else
        {
            var black = Resources.Load<Sprite>("AvatarAssets/BlackAvatarBackground");
            avatarBorder.sprite = black;
        }
    }
}
