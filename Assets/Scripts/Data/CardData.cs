using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Card Data")]
public class CardData : ScriptableObject
{
    public enum CardEffectType { SPELLAOE, SPELLSINGLE, BUFFAOE, BUFFSINGLE, ENTITY, NONUSABLE };
    public enum CardType { GUILE, MALEVOLENCE, BENEVOLENCE };
    public CardEffectType cardEffectType;
    public CardType cardType;
    public string cardDesc;
    public Sprite sprite;

    // 可以是伤害，回复数量 SPELLAOE SPELLSINGLE
    public float EffectVar;
    // 释放范围，不是aoe的设置为0
    public float EffectRange;
    // 释放Buff/Modifier

    // 带有的Entity


}
