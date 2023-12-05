using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffScript : MonoBehaviour
{
    public Image debuffImage;

    public void SetDebuffData(DebuffModel debuff)
    {
        Sprite debuffSprite = Resources.Load<Sprite>(debuff.ImagePath);
        debuffImage.sprite = debuffSprite;
    }

    void Update()
    {
        
    }
}
