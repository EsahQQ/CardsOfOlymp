using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyComponents
{
    public class EnemyVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyName;
        [SerializeField] private Slider enemyHealth;
        [SerializeField] private Image enemyImage;
        [SerializeField] private TextMeshProUGUI enemyAbilityDescription;

        public void Init(EnemyData enemyData)
        {
            enemyName.text = enemyData.enemyName;
            enemyHealth.maxValue = enemyData.healthPoints;
            enemyHealth.value =  enemyData.healthPoints;
            enemyImage.sprite = enemyData.image;
            enemyAbilityDescription.text = enemyData.ability.AbilityDescription;
        }

        private void Start()
        {
            EnemyController.Instance.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object sender, int e)
        {
            enemyHealth.value = e;
        }

        private void OnDestroy()
        {
            EnemyController.Instance.OnHealthChanged -= OnHealthChanged;
        }
    }
}