using System;
using BattleComponents;
using CardComponents.Visual;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIDropManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardsInDropCountText;
        
        private LogicalDrop _logicalDrop;
        
        public void LinkToDropModel(LogicalDrop logicalDrop)
        {
            _logicalDrop =  logicalDrop;
            ChangeCardsInDropCountText();
            _logicalDrop.OnDropChange += OnDropChange;
            CardAnimator.Instance.Init(_logicalDrop);
        }

        private void OnDropChange(object sender, EventArgs e)
        {
            ChangeCardsInDropCountText();
        }

        private void ChangeCardsInDropCountText()
        {
            cardsInDropCountText.text = _logicalDrop.CardCount.ToString();
        }

        public void ShowCardsInDrop()
        {
            var cards = _logicalDrop.GetCards();
            foreach (var card in cards)
            {
                Debug.Log(card.cardName);
            }
        }
    }
}