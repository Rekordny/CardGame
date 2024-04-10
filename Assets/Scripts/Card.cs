using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡牌的实体容器，会在DeckManager初始化的时候将卡牌赋予Data的数值并且加入list
public class Card : MonoBehaviour
{
    public CardData.CardEffectType cardEffectType;
    public CardData.CardType cardType;
    public float EffectVar;
    public float EffectRange;
}
