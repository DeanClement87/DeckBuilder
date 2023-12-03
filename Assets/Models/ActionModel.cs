using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Action")]
public class ActionModel : ScriptableObject
{
    public ActionEnum Action;
    public ActionTargetEnum Target;
    public int Value;
    public string ActionDataText;
}
