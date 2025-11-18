using BattleComponents;
using UI;
using UnityEngine;
using CardComponents;

namespace Core
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Настройки Боя")]
        [SerializeField] private int maxHandSize = 5;
        [SerializeField] private int startingHandSize = 4;
        [SerializeField] private int cardsPerTurn = 1;
        [SerializeField] private int startMana = 6;
        [SerializeField] private int startTurns = 3;
        [SerializeField] private int manaPerTurn = 1;
        
        [Header("Ссылки на View")]
        [SerializeField] private UIHandManager uiHandManager;
        [SerializeField] private UIDeckManager uiDeckManager;
        [SerializeField] private UIManaManager uiManaManager;
        [SerializeField] private UITurnsManager uiTurnsManager;
        [SerializeField] private UIDropManager uiDropManager;
        [SerializeField] private Transform playerField;
        [SerializeField] private Transform enemyContainer;
        
        private LogicalDeck _deck;
        private LogicalHand _hand;
        private LogicalDrop _drop;
        private LogicalMana _mana;
        private LogicalTurns _turns;

        private void Start()
        {
            DeckData currentRunDeck = ScriptableObject.CreateInstance<DeckData>();
            currentRunDeck.cards = GameProgressionManager.Instance.PlayerDeck;
        
            _deck = new LogicalDeck(currentRunDeck);
            
            _hand = new LogicalHand(maxHandSize);
            _drop = new LogicalDrop();
            _mana = new LogicalMana(startMana);
            _turns = new LogicalTurns(startTurns);
            
            uiHandManager.LinkToHandModel(_hand);
            uiDeckManager.LinkToDeckModel(_deck);
            uiManaManager.LinkToManaModel(_mana);
            uiTurnsManager.LinkToTurnsModel(_turns);
            uiDropManager.LinkToDropModel(_drop);
            _deck.LinkToHandModel(_hand);
            
            StartGame();
        }

        private void StartGame()
        {
            _deck.TakeCards(startingHandSize);
        }
        
        public void EndPlayerTurn()
        {
            _mana.AddMana(manaPerTurn);
            var cardsOnField = playerField.GetComponentsInChildren<Card>();
            var enemy = enemyContainer.GetComponentInChildren<EnemyComponents.EnemyController>();

            if (enemy == null) return;

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
                    card.CardData.ability.Execute(new BattleContext
                    {
                        Enemy = enemy,
                        PlayerHand = _hand,
                        PlayerDeck = _deck,
                        PlayerDrop = _drop,
                        PlayerMana = _mana,
                        PlayerTurns = _turns,
                        HandManager = uiHandManager,
                    });
                }
            }
            
            if (enemy.CurrentHealth > 0)
            {
                enemy.GetData().ability.Execute(new BattleContext
                {
                    Enemy = enemy,
                    PlayerHand = _hand,
                    PlayerDeck = _deck,
                    PlayerDrop = _drop,
                    PlayerMana = _mana,
                    PlayerTurns = _turns,
                    HandManager = uiHandManager
                });
            }
            else
            {
                OnBattleOver(true);
                Debug.Log("Enemy dead :(");
                //анимация смерти
                //переход в другую сцену    
                return;
            }
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null)
                {
                    //uiHandManager.MoveCardToHand(card); // !!!
                    _hand.RemoveCard(card);
                    _drop.AddCard(card);
                }
            }
            
            _turns.AddTurns(-1);

            if (_turns.CurrentTurns <= 0)
            {
                OnBattleOver(false);
                Debug.Log("You Lose! Turns left");
            }
            
            _deck.TakeCards(cardsPerTurn);
            
        }
        
        private void OnBattleOver(bool playerWon)
        {
            if (playerWon)
            {
                GameProgressionManager.Instance.BattleWon();
            }
            else
            {
                GameProgressionManager.Instance.BattleLost();
            }
        }
    }
}