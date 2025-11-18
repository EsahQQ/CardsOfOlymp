// AbilityAnimator.cs
using System.Collections;
using UnityEngine;

public class AbilityAnimator : MonoBehaviour
{
    public static AbilityAnimator Instance { get; private set; }

    // Ссылки на цели для анимаций
    [Header("Цели для анимаций")]
    [SerializeField] private Transform enemyVFXTarget;
    [SerializeField] private Transform playerVFXTarget;
    [SerializeField] private Transform background;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Пример корутины для анимации урона
    public IEnumerator PlayVFX(Transform target, GameObject vfxPrefab)
    {
        if (vfxPrefab != null && target != null)
        {
            var duration = vfxPrefab.GetComponentInChildren<ParticleSystem>().main.duration;
            GameObject effectInstance = Instantiate(vfxPrefab, target.position, Quaternion.identity, background);
            
            Debug.Log(duration);
            yield return new WaitForSeconds(duration);
            
            Destroy(effectInstance);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    // Геттеры для целей, чтобы способности могли их получить
    public Transform GetEnemyVFXTarget() => enemyVFXTarget;
    public Transform GetPlayerVFXTarget() => playerVFXTarget;
}