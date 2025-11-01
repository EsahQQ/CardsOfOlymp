using System;
using System.Collections.Generic;
using CardComponents;

namespace BattleComponents
{
    public class LogicalHand
    {
        public List<CardData> CardsInHand { get; private set; }
        public int MaxHandSize { get; private set; }
        
        public event Action<CardData> OnCardAdded;
        public event Action<Card> OnCardRemoved;

        public LogicalHand(int maxHandSize)
        {
            MaxHandSize = maxHandSize;
            CardsInHand = new List<CardData>();
        }

        public bool TryAddCard(CardData card)
        {
            if (CardsInHand.Count >= MaxHandSize) return false; 

            CardsInHand.Add(card);
            OnCardAdded?.Invoke(card);
            return true;
        }

        public void RemoveCard(Card card)
        {
            if (CardsInHand.Remove(card.CardData))
            {
                OnCardRemoved?.Invoke(card); 
            }
        }
    }
}