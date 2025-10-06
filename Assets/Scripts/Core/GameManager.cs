using Battle;
using UI;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Hand handManager;
        [SerializeField] private UIHandManager UIHandManager;
        private void Start()
        {
            UIHandManager.Init();
            handManager.Init();
        }
    }
}
