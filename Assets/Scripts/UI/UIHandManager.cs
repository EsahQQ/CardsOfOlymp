using System;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class UIHandManager : MonoBehaviour
    {
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject cardPrefab;
        
        public void Init()
        {
            Hand.Instance.OnCardTaken += OnCardTaken;
        }

        private void OnDestroy()
        {
            Hand.Instance.OnCardTaken -= OnCardTaken;
        }

        private void OnCardTaken(object sender, CardData e)
        {
            SpawnCardInHand(e);
        }

        private void SpawnCardInHand(CardData cardData)
        {
            var card = Instantiate(cardPrefab, hand.transform);
            card.SetActive(true);
        }
    }
}