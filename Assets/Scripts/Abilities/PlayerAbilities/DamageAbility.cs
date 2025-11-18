using System.Collections;
using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.PlayerAbilities
{
    [CreateAssetMenu(fileName = "New DamageAbility", menuName = "Ability/DamageAbility")]
    public class DamageAbility : Ability
    {
        public int damage;
        public GameObject vfxPrefab;
        public override IEnumerator Execute(BattleContext context)
        {
            var enemyHitTake = context.Enemy.EnemyHitTake;
            
            if (context.Enemy != null)
            {
                yield return AbilityAnimator.Instance.PlayVFX(enemyHitTake, vfxPrefab);
                
                context.Enemy.TakeDamage(damage);
            }
        }
    }
}