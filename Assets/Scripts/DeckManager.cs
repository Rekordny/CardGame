using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Card[] deck;
    public int deckCapacity = 10;


    private void Start()
    {
        deck = new Card[deck.Length];
    }
}
