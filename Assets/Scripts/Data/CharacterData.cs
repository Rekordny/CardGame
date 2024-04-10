using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _attack;
    [SerializeField]
    private int _defense;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _attackSpeed;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    public int Defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }
}
