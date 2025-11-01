using System.Collections.Generic;
using CardComponents;
using UnityEngine; 

namespace BattleComponents
{
    public class LogicalDeck
    {
        private List<CardData> _cardsInDeck;

        private LogicalHand _logicalHand;    
        
        public int CardCount => _cardsInDeck.Count;

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

        public CardData DrawCard()
        {
            if (_cardsInDeck.Count == 0) return null;

            var drawnCard = _cardsInDeck[0];
            _cardsInDeck.RemoveAt(0);
            return drawnCard;
        }

        public void TakeCards(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var card = _cardsInDeck[0];
                _logicalHand.TryAddCard(card);
                _cardsInDeck.RemoveAt(0);
            }
            
        }
    }
}