using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        public int enemyName;
        public int healthPoints;
    }
}
