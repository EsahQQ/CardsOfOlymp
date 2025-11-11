using System;
using BattleComponents;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIDeckManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardsInDeckCountText;
        
        private LogicalDeck _logicalDeck;
        
        public void LinkToDeckModel(LogicalDeck logicalDeck)
        {
            _logicalDeck =  logicalDeck;
            ChangeCardsInDeckCountText();
            _logicalDeck.OnDeckChange += OnDeckChange;
        }

        private void OnDeckChange(object sender, EventArgs e)
        {
            ChangeCardsInDeckCountText();
        }

        private void ChangeCardsInDeckCountText()
        {
            cardsInDeckCountText.text = _logicalDeck.CardCount.ToString();
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