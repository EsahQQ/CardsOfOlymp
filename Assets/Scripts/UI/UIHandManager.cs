using System.Collections.Generic;
using System.Linq;
using BattleComponents;
using CardComponents;
using CardComponents.Visual;
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
            cardObject.GetComponent<Card>().Init(cardData);
            cardObject.SetActive(false);
            CardAnimator.Instance.QueueAnimation(
                CardAnimator.Instance.AnimateCardToHand(cardObject, handContainer)
            );
        }
        
        private void OnCardRemovedFromHand(Card card)
        {
            
        }

        public void MoveCardToHand(Card card)
        {
            card.transform.SetParent(handContainer);
        }

        public List<Card> GetCardsInHand()
        {
            return handContainer.GetComponentsInChildren<Card>().ToList();
        }
    }
}