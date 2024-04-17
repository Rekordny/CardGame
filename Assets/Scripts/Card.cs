using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CardData;

// 卡牌的实体容器，会在DeckManager初始化的时候将卡牌赋予Data的数值并且加入list
public class Card : MonoBehaviour
{
    public CardData.CardEffectType cardEffectType;
    public CardData.CardType cardType;
    public float effectVar;
    public float effectRange;
    public string cardName;
    public Sprite cardSprite;
    public string cardDesc;

    public TMP_Text cardNameReference;
    public TMP_Text cardDescReference;
    public Image cardImageReference;

    public void initialize(CardData cardData)
    {
        // 把数据拿进来mono behavior
        cardEffectType = cardData.cardEffectType;
        cardType = cardData.cardType;
        effectVar = cardData.EffectVar;
        effectRange = cardData.EffectRange;
        cardName = cardData.name;
        cardSprite = cardData.sprite;
        cardDesc = cardData.cardDesc;

        // 获得Prefab的Reference并且更新prefab reference再加上视觉的text,卧槽原始人
        cardImageReference = transform.GetChild(0).gameObject.GetComponent<Image>();
        cardNameReference = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        cardDescReference = transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

        cardImageReference.sprite = cardSprite;
        cardNameReference.text = cardName;
        cardDescReference.text = cardDesc;
    }


    // 施加效果
    public void applyEffectToCharacterList(List<Character> characters)
    {
        if (characters != null)
        {
            foreach (Character character in characters)
            {
                switch (cardEffectType)
                {
                    case CardEffectType.SPELLAOE:
                    case CardEffectType.SPELLSINGLE:
                        if (effectVar >= 0)
                            character.TakeDamage(effectVar);
                        else character.TakeHeal(effectVar);
                        break;
                    case CardEffectType.BUFFAOE:
                    case CardEffectType.BUFFSINGLE:
                        break;
                    case CardEffectType.ENTITY:
                        break;
                    case CardEffectType.NONUSABLE:
                        break;
                }
            }
        }
    }

    public void useThisCard()
    {
        HandManager.Instance.UsedFromHand(this);
        GameObject.Destroy(this.gameObject);
    }
}
