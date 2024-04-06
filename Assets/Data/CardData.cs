using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Card Data")]
public class CardData : ScriptableObject
{
    public enum CardType { SPELLAOE, SPELLSINGLE, BUFFAOE, BUFFSINGLE, ENTITY };
    public enum CardCategory { };
}
