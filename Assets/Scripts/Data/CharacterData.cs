using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float maxHealth;
    public float attack;
    public float defense;
    public float moveSpeed;
    public float attackRange;
    public float attackSpeed;

}
