using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Base Card")]
public class BaseCardModel : ScriptableObject
{
    public int Id;
    public string CardName;
    public string Description;
    public string Image;
    public int ManaCost;
    public List<ActionModel> Actions;
    public List<CardConditionEnum> CardConditionEnums;
}
