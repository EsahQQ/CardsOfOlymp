using CardComponents;
using Core;
using EnemyComponents;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New RemoveRandomCardFromHandToDropAbility", menuName = "Ability/RemoveRandomCardFromHandToDropAbility")]
    public class RemoveRandomCardFromHandToDropAbility : Ability
    {
        public override void Execute(BattleContext context)
        {
            var hand = context.HandManager;
            var logicalHand = context.PlayerHand;
            var logicalDrop = context.PlayerDrop;
            if (hand != null)
            {
                var rnd = Random.Range(0, hand.GetCardsInHand().Count);
                var card = hand.GetCardsInHand()[rnd];
                Debug.Log(rnd);
                logicalHand.RemoveCard(card);
                logicalDrop.AddCard(card.CardData);
            }
        }
    }
}