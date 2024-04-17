using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField]
    private List<CardData> registeredCards;
    public List<CardData> deck;
    public int deckCapacity = 15;
    public int startHandNum = 5;

    public override void Awake()
    {
        base.Awake();
        deck = new List<CardData>();
    }

    private void Start()
    {
        
        InitializeDeck();

        for (int i = 0; i < startHandNum; i++) 
        {
            DrawACardFromDeck();
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawACardFromDeck();
        }
    }

    private void InitializeDeck()
    {
        for (int i = 0; i < deckCapacity; i++)
        {
            deck.Add(registeredCards[Random.Range(0,registeredCards.Count)]);
        }
    }

    private void DrawACardFromDeck()
    {
        if(deck.Count > 0)
        {
            int randomInt = Random.Range(0, deck.Count);
            

            CardData temp = deck[randomInt];
            deck.RemoveAt(randomInt);

            HandManager.Instance.AddToHand(temp);
            
        }
        else
        {
            // byd没得抽了
        }
    }
}
