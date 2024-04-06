using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Deck Data")]
public class DeckData : ScriptableObject
{
    private CardData[] Cards;
    private int currentCard = 0;
}
