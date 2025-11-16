using System;
using System.Collections.Generic;
using CardComponents;
using UnityEngine;

namespace BattleComponents
{
    public class LogicalDrop
    {
        private List<CardData> _cardsInDrop = new();
        
        public int CardCount => _cardsInDrop.Count;
        public event EventHandler OnDropChange;
        public event EventHandler<Card> OnCardAddToDrop;

        public bool AddCard(Card card)
        {
            _cardsInDrop.Add(card.CardData);
            OnDropChange?.Invoke(this, EventArgs.Empty);
            OnCardAddToDrop?.Invoke(this, card);
            return true;
        }

        public void RemoveCard(Card card)
        {
            if (_cardsInDrop.Remove(card.CardData))
            {
                OnDropChange?.Invoke(this, EventArgs.Empty); 
            }
        }
        
        public List<CardData> GetCards() => _cardsInDrop;
    }
}