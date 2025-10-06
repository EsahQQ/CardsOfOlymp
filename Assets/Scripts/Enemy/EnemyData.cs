using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int healthPoints;
        public Sprite image;
    }
}
