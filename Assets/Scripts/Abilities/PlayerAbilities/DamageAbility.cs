using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.PlayerAbilities
{
    [CreateAssetMenu(fileName = "New DamageAbility", menuName = "Ability/DamageAbility")]
    public class DamageAbility : Ability
    {
        public int damage;
        public override void Execute(BattleContext context)
        {
            if (context.Enemy != null)
            {
                context.Enemy.TakeDamage(damage);
            }
        }
    }
}