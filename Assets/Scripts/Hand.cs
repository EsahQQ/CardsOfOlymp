using System;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

public class Hand : MonoBehaviour
{
    [SerializeField] private int startHandLength = 4;
    private List<CardData> _currentHand;
    
    public static Hand Instance { get; private set; }
    
    public event EventHandler<CardData> OnCardTaken;

    private void Awake()
    {
        _currentHand = new List<CardData>();
        Instance = this;
    }

    public void Init()
    {
        for (int i = 0; i < startHandLength; i++)
        {
            var card = Deck.Instance.TakeCard();
            _currentHand.Add(card);
            OnCardTaken?.Invoke(this, card);
        }
    }
}