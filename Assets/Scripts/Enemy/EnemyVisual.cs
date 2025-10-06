using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyName;
        [SerializeField] private Slider enemyHealth;
        [SerializeField] private Image enemyImage;

        public void Init(EnemyData enemyData)
        {
            enemyName.text = enemyData.enemyName;
            enemyHealth.maxValue = enemyData.healthPoints;
            enemyImage.sprite = enemyData.image;
        }
    }
}