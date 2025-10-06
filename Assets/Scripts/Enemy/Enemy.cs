using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
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
    }
}
