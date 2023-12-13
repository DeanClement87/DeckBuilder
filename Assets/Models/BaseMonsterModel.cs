using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Base Monster")]
public class BaseMonsterModel : ScriptableObject
{
    public int Id;
    public string MonsterName;
    public string Description;
    public string Image;
    public int Health;
    public int Attack;
    public List<MonsterAttributeEnum> MonsterAttributes;
    public ParticleEnum ParticleEnum;
    public ParticleBehavourEnum ParticleBehavourEnum;
}
