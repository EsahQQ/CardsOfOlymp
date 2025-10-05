using System;
using ScriptableObjects;
using UI;
using UnityEngine;

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
