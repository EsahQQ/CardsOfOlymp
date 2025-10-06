using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Battle
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private DeckData startDeck;

        private List<CardData> _currentDeck;
    
        public static Deck Instance { get; private set; }
        private void Awake()
        {
            _currentDeck = new List<CardData>();
            foreach (var card in startDeck.cards)
            {
                _currentDeck.Add(card);
            }
        
            Instance = this;
        }

        public CardData TakeCard()
        {
            var card = _currentDeck[Random.Range(0, _currentDeck.Count)];
            _currentDeck.Remove(card);
            return card;
        }
    }
}
