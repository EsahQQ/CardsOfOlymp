using BattleComponents;
using UnityEngine;

namespace UI
{
    public class UIDeckManager : MonoBehaviour
    {
        private LogicalDeck _logicalDeck;
        
        public void LinkToDeckModel(LogicalDeck logicalDeck)
        {
            _logicalDeck =  logicalDeck;
        }
        
        public void ShowCardsInDeck()
        {
            var cards = _logicalDeck.GetCards();
            foreach (var card in cards)
            {
                Debug.Log(card.cardName);
            }
        }
    }
}