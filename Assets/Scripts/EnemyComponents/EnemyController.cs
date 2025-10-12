using System;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;

        private EnemyVisual _visual; 
        
        private void Awake()
        {
            _visual = GetComponent<EnemyVisual>();
        }
        private void Start()
        {
            _visual.Init(enemyData);
        }

        public void TakeDamage(int damage)
        {
            
        }
    }
}
