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

        public bool AddCard(CardData card)
        {
            _cardsInDrop.Add(card);
            OnDropChange?.Invoke(this, EventArgs.Empty);
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