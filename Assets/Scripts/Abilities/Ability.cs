using UnityEngine;
using EnemyComponents;

namespace CardComponents.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public string AbilityName;
        
        public abstract void Execute(GameObject target); 
    }
}