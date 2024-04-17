using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HandManager : Singleton<HandManager>
{
    private List<Card> hand;
    public GameObject cardPrefab;
    public GameObject handReference;
    public override void Awake()
    {
        base.Awake();
        hand = new List<Card>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddToHand(CardData cardData)
    {
        
        Card newCard = Instantiate(cardPrefab).AddComponent<Card>();
        hand.Add(newCard);
        newCard.initialize(cardData);
        newCard.transform.SetParent(handReference.transform);
        
    }

    public void UsedFromHand(Card card)
    {
        hand.Remove(card);
    }

}
