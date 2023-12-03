using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public Guid Id { get; set; }
    public BaseCardModel BaseCard { get; set; }

    public CardModel(BaseCardModel baseCard)
    {
        Id = Guid.NewGuid();
        BaseCard = baseCard;
    }
}
