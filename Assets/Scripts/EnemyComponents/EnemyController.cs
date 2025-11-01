using System;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;

        private EnemyVisual _visual;

        public static EnemyController Instance { get; private set; }
        
        public event EventHandler<int> OnHealthChanged;
        
        public int CurrentHealth { get; private set; }
        
        private void Awake()
        {
            _visual = GetComponent<EnemyVisual>();
            Instance = this;
        }
        private void Start()
        {
            CurrentHealth = enemyData.healthPoints;
            _visual.Init(enemyData);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
            
            OnHealthChanged?.Invoke(this, CurrentHealth);
        }
    }
}
