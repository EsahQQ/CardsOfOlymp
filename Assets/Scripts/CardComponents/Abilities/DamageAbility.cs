using EnemyComponents;
using UnityEngine;

namespace CardComponents.Abilities
{
    [CreateAssetMenu(fileName = "New DamageAbility", menuName = "Ability/DamageAbility")]
    public class DamageAbility : Ability
    {
        public float Damage;
        public override void Execute(GameObject target)
        {
            var enemy = target.GetComponent<EnemyController>();
            if (enemy)
            {
                //enemy.TakeDamage(Damage);
            }
        }
    }
}