using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡牌的实体容器，会在DeckManager初始化的时候将卡牌赋予Data的数值并且加入list
public class Card : MonoBehaviour
{
    public CardData.CardEffectType cardEffectType;
    public CardData.CardType cardType;
    public float effectVar;
    public float effectRange;
    public string cardName;
    public Sprite cardSprite;

    public void selfInitialize(CardData cardData)
    {
        cardEffectType = cardData.cardEffectType;
        cardType = cardData.cardType;
        effectVar = cardData.EffectVar; 
        effectRange = cardData.EffectRange;
        cardName = cardData.name;
        cardSprite = cardData.sprite;
    }
}
