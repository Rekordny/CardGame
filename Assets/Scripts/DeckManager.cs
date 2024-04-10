using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Card[] deck;
    public int deckCapacity = 10;
    public CardData FireBall;
    public CardData HealthPotion;


    private void Start()
    {
        deck = new Card[deckCapacity];
    }

    private void InitializeDeck()
    {
        for (int i = 0; i < deckCapacity; i++)
        {
            deck[i] = new Card();
            float random = Random.value;
            if(random > 0.5f)
            {
                deck[i].selfInitialize(FireBall);
            }
            else
            {
                deck[i].selfInitialize(HealthPotion);
            }
            
            
        }
    }
}
