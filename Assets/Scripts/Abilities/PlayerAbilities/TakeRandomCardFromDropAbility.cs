using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.PlayerAbilities
{
    [CreateAssetMenu(fileName = "New TakeRandomCardFromDropAbility", menuName = "Ability/TakeRandomCardFromDropAbility")]
    public class TakeRandomCardFromDropAbility : Ability
    {
        public int cardCounts;
        public override void Execute(BattleContext context)
        {
            var logicalDrop = context.PlayerDrop;
            var logicalHand = context.PlayerHand;
            if (logicalDrop.CardCount > 0)
            {
                var rnd = Random.Range(0, logicalDrop.GetCards().Count);
                var cardData = logicalDrop.GetCards()[rnd];
                logicalDrop.RemoveCard(cardData);
                logicalHand.TryAddCard(cardData);
            }
        }
    }
}