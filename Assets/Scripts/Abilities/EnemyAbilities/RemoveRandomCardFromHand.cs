using CardComponents;
using Core;
using EnemyComponents;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New RemoveRandomCardFromHandAbility", menuName = "Ability/RemoveRandomCardFromHandAbility")]
    public class RemoveRandomCardFromHandAbility : Ability
    {
        public override void Execute(BattleContext context)
        {
            var hand = context.HandManager;
            var logicalHand = context.PlayerHand;
            if (hand != null)
            {
                var rnd = Random.Range(0, hand.GetCardsInHand().Count);
                Debug.Log(rnd);
                logicalHand.RemoveCard(hand.GetCardsInHand()[rnd]);
            }
        }
    }
}