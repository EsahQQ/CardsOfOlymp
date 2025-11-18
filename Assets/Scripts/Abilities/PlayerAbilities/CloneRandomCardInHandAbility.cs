using System.Collections;
using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.PlayerAbilities
{
    [CreateAssetMenu(fileName = "New CloneRandomCardInHandAbility", menuName = "Ability/CloneRandomCardInHandAbility")]
    public class CloneRandomCardInHandAbility : Ability
    {
        public int cloneCounts;
        public GameObject vfxPrefab;
        public override IEnumerator Execute(BattleContext context)
        {
            var handManager = context.HandManager;
            var logicalHand = context.PlayerHand;
            if (handManager.GetCardsInHand().Count > 0)
            {
                var rnd = Random.Range(0, handManager.GetCardsInHand().Count);
                yield return AbilityAnimator.Instance.PlayVFX(handManager.GetCardsInHand()[rnd].transform, vfxPrefab);
                
                for (var i = 0; i < cloneCounts; i++)
                    logicalHand.TryAddCard(handManager.GetCardsInHand()[rnd].CardData);
            }
        }
    }
}