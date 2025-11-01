using BattleComponents;
using UI;
using UnityEngine;
using CardComponents;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Настройки Боя")]
        [SerializeField] private DeckData startingDeckData;
        [SerializeField] private int maxHandSize = 5;
        [SerializeField] private int startingHandSize = 4;
        
        [Header("Ссылки на View")]
        [SerializeField] private UIHandManager uiHandManager;
        [SerializeField] private Transform playerField;
        [SerializeField] private Transform enemyContainer;
        
        private LogicalDeck _deck;
        private LogicalHand _hand;
        private LogicalDrop _drop;

        private void Start()
        {
            _deck = new LogicalDeck(startingDeckData);
            _hand = new LogicalHand(maxHandSize);
            _drop = new LogicalDrop();
            
            uiHandManager.LinkToHandModel(_hand);
            
            
            StartGame();
        }

        private void StartGame()
        {
            for (int i = 0; i < startingHandSize; i++)
            {
                AddCard();
            }
        }
        
        public void AddCard()
        {
            CardData cardToDraw = _deck.DrawCard();
            if (cardToDraw != null)
            {
                _hand.TryAddCard(cardToDraw);
            }
            else
            {
                Debug.Log("Колода пуста!");
            }
        }
        
        public void EndPlayerTurn()
        {
            var cardsOnField = playerField.GetComponentsInChildren<Card>();
            var targetEnemy = enemyContainer.GetComponentInChildren<EnemyComponents.EnemyController>();

            if (targetEnemy == null) return;
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null && card.CardData.ability != null)
                {
                    card.CardData.ability.Execute(targetEnemy.gameObject);
                    _hand.RemoveCard(card);
                    _drop.AddCard(card.CardData);
                }
            }

            if (targetEnemy.CurrentHealth > 0)
            {
                
            }
        }
    }
}