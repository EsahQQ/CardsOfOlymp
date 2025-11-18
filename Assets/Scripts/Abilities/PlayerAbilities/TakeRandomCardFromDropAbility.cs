using System.Collections;
using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.PlayerAbilities
{
    [CreateAssetMenu(fileName = "New TakeRandomCardFromDropAbility", menuName = "Ability/TakeRandomCardFromDropAbility")]
    public class TakeRandomCardFromDropAbility : Ability
    {
        public int cardCounts;
        public GameObject vfxPrefab;
        public override IEnumerator Execute(BattleContext context)
        {
            var logicalDrop = context.PlayerDrop;
            var logicalHand = context.PlayerHand;
            var dropUI = context.DropManager.DropContainer;
            if (logicalDrop.CardCount > 0)
            {
                yield return AbilityAnimator.Instance.PlayVFX(dropUI.transform, vfxPrefab);
                
                var rnd = Random.Range(0, logicalDrop.GetCards().Count);
                var cardData = logicalDrop.GetCards()[rnd];
                logicalDrop.RemoveCard(cardData);
                logicalHand.TryAddCard(cardData);
            }
        }
    }
}