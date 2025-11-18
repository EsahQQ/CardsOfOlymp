using System.Collections;
using Core;
using UnityEngine;

namespace Abilities.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New RemoveRandomCardFromHandToDropAbility", menuName = "Ability/RemoveRandomCardFromHandToDropAbility")]
    public class RemoveRandomCardFromHandToDropAbility : Ability
    {
        public GameObject vfxPrefab;
        public override IEnumerator Execute(BattleContext context)
        {
            var handManager = context.HandManager;
            var logicalHand = context.PlayerHand;
            var logicalDrop = context.PlayerDrop;
            if (handManager.GetCardsInHand().Count > 0)
            {
                var rnd = Random.Range(0, handManager.GetCardsInHand().Count);
                var card = handManager.GetCardsInHand()[rnd];
                
                yield return AbilityAnimator.Instance.PlayVFX(card.transform, vfxPrefab);
                
                logicalHand.RemoveCard(card);
                logicalDrop.AddCard(card);
            }
        }
    }
}