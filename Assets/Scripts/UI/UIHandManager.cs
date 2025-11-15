using System.Collections.Generic;
using System.Linq;
using BattleComponents;
using CardComponents;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class UIHandManager : MonoBehaviour
    {
        [SerializeField] private Transform handContainer; 
        [SerializeField] private GameObject cardPrefab;

        private LogicalHand _linkedHandModel;
        
        public void LinkToHandModel(LogicalHand handModel)
        {
            _linkedHandModel = handModel;

            _linkedHandModel.OnCardAdded += OnCardAddedToHand;
            _linkedHandModel.OnCardRemoved += OnCardRemovedFromHand;
        }

        private void OnDestroy()
        {
            if (_linkedHandModel != null)
            {
                _linkedHandModel.OnCardAdded -= OnCardAddedToHand;
                _linkedHandModel.OnCardRemoved -= OnCardRemovedFromHand;
            }
        }

        private void OnCardAddedToHand(CardData cardData)
        {
            var cardObject = Instantiate(cardPrefab, handContainer);
            Debug.Log($"{cardData.cardName} added to handContainer");
            cardObject.GetComponent<Card>().Init(cardData);
        }
        
        private void OnCardRemovedFromHand(Card card)
        {
            Debug.Log($"{card.CardData.cardName} destroyed");
            card.gameObject.SetActive(false);
            Destroy(card.gameObject);
        }

        public void MoveCardToHand(Card card)
        {
            Debug.Log($"{card.CardData.cardName} moved to HandContainer");
            card.transform.SetParent(handContainer);
        }

        public List<Card> GetCardsInHand()
        {
            return handContainer.GetComponentsInChildren<Card>().ToList();
        }
    }
}