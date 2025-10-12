using BattleComponents;
using CardComponents;
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
        }

        private void OnCardRemovedFromHand(CardData cardData)
        {
            // Здесь логика удаления GameObject'а карты из руки
        }
    }
}