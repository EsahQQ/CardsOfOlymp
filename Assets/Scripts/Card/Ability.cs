using UnityEngine;


namespace Card
{
    public abstract class Ability : ScriptableObject
    {
        public string abilityName;
        
        public abstract void Execute(GameObject target); 
    }
}