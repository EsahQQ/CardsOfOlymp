using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class AbilityAnimator : MonoBehaviour
    {
        public static AbilityAnimator Instance { get; private set; }

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

        public IEnumerator PlayVFX(Transform target, GameObject vfxPrefab)
        {
            if (vfxPrefab != null && target != null)
            {
                var duration = vfxPrefab.GetComponentInChildren<ParticleSystem>().main.duration;
                var effectInstance = Instantiate(vfxPrefab, target.position, Quaternion.identity, background);

                yield return new WaitForSeconds(duration);
            
                Destroy(effectInstance);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

        public Transform GetEnemyVFXTarget() => enemyVFXTarget;
        public Transform GetPlayerVFXTarget() => playerVFXTarget;
    }
}