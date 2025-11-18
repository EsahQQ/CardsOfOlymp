using System.Collections;
using BattleComponents;
using UI;
using UnityEngine;
using CardComponents;
using CardComponents.Visual;

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
        
        private bool isTurnProcessing = false;

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
            if (isTurnProcessing) return; // Если ход уже обрабатывается, ничего не делаем
            
            StartCoroutine(EndPlayerTurnSequence());
        }

        private IEnumerator EndPlayerTurnSequence()
        {
            var cardsOnField = playerField.GetComponentsInChildren<Card>();
            var enemy = enemyContainer.GetComponentInChildren<EnemyComponents.EnemyController>();

            if (enemy == null)
            {
                isTurnProcessing = false;
                yield break; // Выходим из корутины
            }

            var neededMana = 0;
            
            foreach (var card in cardsOnField)
            {
                neededMana += card.CardData.manaCost;
            }
            
            if (_mana.CurrentMana < neededMana)
            {
                Debug.Log("Not enough mana!");
                isTurnProcessing = false;
                yield break;
            }
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null && card.CardData.ability != null)
                {
                    yield return StartCoroutine(card.CardData.ability.Execute(new BattleContext
                    {
                        Enemy = enemy,
                        PlayerHand = _hand,
                        PlayerDeck = _deck,
                        PlayerDrop = _drop,
                        PlayerMana = _mana,
                        PlayerTurns = _turns,
                        HandManager = uiHandManager,
                        DeckManager = uiDeckManager,
                        DropManager = uiDropManager,
                    }));
                }
            }
            
            if (enemy.CurrentHealth > 0)
            {
                yield return new WaitForSeconds(0.5f);
                
                yield return StartCoroutine(enemy.GetData().ability.Execute(new BattleContext
                {
                    Enemy = enemy,
                    PlayerHand = _hand,
                    PlayerDeck = _deck,
                    PlayerDrop = _drop,
                    PlayerMana = _mana,
                    PlayerTurns = _turns,
                    HandManager = uiHandManager,
                    DeckManager = uiDeckManager,
                    DropManager = uiDropManager,
                }));
            }
            else
            {
                Debug.Log("Enemy dead :(");
                OnBattleOver(true);
                isTurnProcessing = false;
                yield break;
            }
            
            foreach (var card in cardsOnField)
            {
                if (card.CardData != null)
                {
                    _hand.RemoveCard(card);
                    _drop.AddCard(card);    
                }
            }
            
            yield return new WaitUntil(() => !CardAnimator.Instance.IsAnimating);
            
            _turns.AddTurns(-1);

            if (_turns.CurrentTurns <= 0)
            {
                Debug.Log("You Lose! Turns left");
                OnBattleOver(false);
                isTurnProcessing = false;
                yield break;
            }
            
            _deck.TakeCards(cardsPerTurn);
            _mana.AddMana(manaPerTurn);
            
            yield return new WaitUntil(() => !CardAnimator.Instance.IsAnimating);
            
            isTurnProcessing = false;
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