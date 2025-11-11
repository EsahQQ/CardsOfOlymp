using System.Collections.Generic;
using CardComponents;
using UnityEngine; 
using System;
using Random = UnityEngine.Random;

namespace BattleComponents
{
    public class LogicalDeck
    {
        private List<CardData> _cardsInDeck;

        private LogicalHand _logicalHand;    
        
        public int CardCount => _cardsInDeck.Count;

        public event EventHandler OnDeckChange;

        public LogicalDeck(DeckData startingDeck)
        {
            _cardsInDeck = new List<CardData>(startingDeck.cards);
            Shuffle();
        }

        private void Shuffle()
        {
            for (var i = 0; i < _cardsInDeck.Count - 1; i++)
            {
                var rnd = Random.Range(i, _cardsInDeck.Count);
                (_cardsInDeck[rnd], _cardsInDeck[i]) = (_cardsInDeck[i], _cardsInDeck[rnd]);
            }
        }

        public void LinkToHandModel(LogicalHand hand)
        {
            _logicalHand = hand;
        }

        public void TakeCards(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var card = _cardsInDeck[0];
                _logicalHand.TryAddCard(card);
                _cardsInDeck.RemoveAt(0);
                OnDeckChange?.Invoke(this, EventArgs.Empty);
            }
        }
        
        public List<CardData> GetCards() => _cardsInDeck;
    }
}