using Core;
using UnityEngine;

namespace Abilities.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New RemoveRandomCardFromHandToDropAbility", menuName = "Ability/RemoveRandomCardFromHandToDropAbility")]
    public class RemoveRandomCardFromHandToDropAbility : Ability
    {
        public override void Execute(BattleContext context)
        {
            var handManager = context.HandManager;
            var logicalHand = context.PlayerHand;
            var logicalDrop = context.PlayerDrop;
            if (handManager.GetCardsInHand().Count > 0)
            {
                var rnd = Random.Range(0, handManager.GetCardsInHand().Count);
                var card = handManager.GetCardsInHand()[rnd];
                logicalHand.RemoveCard(card);
                logicalDrop.AddCard(card);
            }
        }
    }
}