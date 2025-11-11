using Core;
using EnemyComponents;
using UnityEngine;

namespace Abilities.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New RemoveRandomCardFromHandAbility", menuName = "Ability/RemoveRandomCardFromHandAbility")]
    public class RemoveRandomCardFromHandAbility : Ability
    {
        public override void Execute(BattleContext context)
        {
            if (context.PlayerHand != null)
            {
                //context.PlayerHand.RemoveRandomCard(); 
            }
        }
    }
}