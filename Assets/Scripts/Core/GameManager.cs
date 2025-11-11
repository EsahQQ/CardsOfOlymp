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
        [SerializeField] private int cardsPerTurn = 1;
        [SerializeField] private int startMana = 6;
        [SerializeField] private int startTurns = 3;
        
        [Header("Ссылки на View")]
        [SerializeField] private UIHandManager uiHandManager;
        [SerializeField] private UIDeckManager uiDeckManager;
        [SerializeField] private UIManaManager uiManaManager;
        [SerializeField] private UITurnsManager uiTurnsManager;
        [SerializeField] private Transform playerField;
        [SerializeField] private Transform enemyContainer;
        
        private LogicalDeck _deck;
        private LogicalHand _hand;
        private LogicalDrop _drop;
        private LogicalMana _mana;
        private LogicalTurns _turns;

        private void Start()
        {
            _deck = new LogicalDeck(startingDeckData);
            _hand = new LogicalHand(maxHandSize);
            _drop = new LogicalDrop();
            _mana = new LogicalMana(startMana);
            _turns = new LogicalTurns(startTurns);
            
            uiHandManager.LinkToHandModel(_hand);
            uiDeckManager.LinkToDeckModel(_deck);
            uiManaManager.LinkToManaModel(_mana);
            uiTurnsManager.LinkToTurnsModel(_turns);
            _deck.LinkToHandModel(_hand);
            
            StartGame();
        }

        private void StartGame()
        {
            _deck.TakeCards(startingHandSize);
        }
        
        public void EndPlayerTurn()
        {
            var cardsOnField = playerField.GetComponentsInChildren<Card>();
            var targetEnemy = enemyContainer.GetComponentInChildren<EnemyComponents.EnemyController>();

            if (targetEnemy == null) return;

            var neededMana = 0;
            
            foreach (var card in cardsOnField)
            {
                neededMana += card.CardData.manaCost;
            }
            
            if (_mana.CurrentMana < neededMana)
            {
                Debug.Log("Not enough mana!");
                return;
            }
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null && card.CardData.ability != null)
                {
                    card.CardData.ability.Execute(targetEnemy.gameObject);
                    
                }
            }
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null)
                {
                    _hand.RemoveCard(card);
                    _drop.AddCard(card.CardData);
                }
            }
            
            if (targetEnemy.CurrentHealth > 0)
            {
                //Способность енеми
            }
            else
            {
                Debug.Log("Enemy dead :(");
                //анимация смерти
                //переход в другую сцену    
                return;
            }
            
            _deck.TakeCards(cardsPerTurn);
        }
    }
}