using System;
using System.Collections.Generic;
using CardComponents;
using UnityEngine;

namespace BattleComponents
{
    public class LogicalDrop
    {
        public List<CardData> CardsInDrop { get; private set; }
        
        public event Action<CardData> OnCardAdded;
        public event Action<Card> OnCardRemoved;

        public LogicalDrop()
        {
            CardsInDrop = new List<CardData>();
        }

        public bool AddCard(CardData card)
        {
            CardsInDrop.Add(card);
            OnCardAdded?.Invoke(card);
            return true;
        }

        public void RemoveCard(Card card)
        {
            if (CardsInDrop.Remove(card.CardData))
            {
                OnCardRemoved?.Invoke(card); 
            }
        }
    }
}