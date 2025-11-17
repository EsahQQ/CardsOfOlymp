using UnityEngine;
using Core;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public string AbilityDescription;
        public abstract void Execute(BattleContext context); 
    }
}