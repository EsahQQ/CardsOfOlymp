using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public int HealthPoint { get; private set; } = 100;
    }
}
