using System;
using System.Collections;
using System.Collections.Generic;
using CardComponents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameProgressionManager : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private CardPool initialCardPool;
    [SerializeField] private int startingDeckSize = 8;
    [SerializeField] private float sceneTransitionDuration = 0.8f;
    
    public static GameProgressionManager Instance { get; private set; }
    
    public List<CardData> PlayerDeck { get; private set; }
    public int CurrentLevel { get; private set; }
    public int PlayerGold { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        PlayerDeck = new List<CardData>();
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //Instance = null;
    }

    public void StartNewRun()
    {
        Debug.Log("Начинаем новое прохождение!");
        CurrentLevel = 1;
        PlayerGold = 0;
        
        GenerateStartingDeck();
        
        StartNextBattle();
    }

    private void GenerateStartingDeck()
    {
        PlayerDeck.Clear();
        
        List<CardData> availableCards = new List<CardData>(initialCardPool.allCards);
    
        for (int i = 0; i < startingDeckSize; i++)
        {
            if (availableCards.Count == 0) break; 
        
            int randomIndex = Random.Range(0, availableCards.Count);
            CardData randomCard = availableCards[randomIndex];
        
            PlayerDeck.Add(randomCard);
            
            availableCards.RemoveAt(randomIndex);
        }
    
        Debug.Log($"Сгенерирована колода из {PlayerDeck.Count} карт.");
    }

    public void StartNextBattle()
    {
        Debug.Log($"Начинаем бой на уровне {CurrentLevel}");

        LoadScene("Main"); 
    }

    public void BattleWon()
    {
        Debug.Log("Победа в бою!");
        CurrentLevel++;
        PlayerGold += 100; 
        
        LoadScene("ShopScene");
    }

    public void BattleLost()
    {
        Debug.Log("Поражение!");

        LoadScene("MainMenu");
    }

    public void ChangeGold(int amount)
    {
        PlayerGold += amount;
    }

    private void LoadScene(string sceneName)
    {
        var background = GameObject.Find("Background");
        background.transform.DOMove(new Vector3(-1920+960, background.transform.position.y, background.transform.position.z), sceneTransitionDuration).SetEase(Ease.OutQuad);
        
        DOVirtual.DelayedCall(sceneTransitionDuration, () => {
            SceneManager.LoadScene(sceneName);
        });
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var background = GameObject.Find("Background");

        background.transform.position = new Vector3(
            1920+960,
            background.transform.position.y, 
            background.transform.position.z
        );
        
        background.transform.DOMove(new Vector3(960,  background.transform.position.y, background.transform.position.z), sceneTransitionDuration).SetEase(Ease.OutQuad);
    }
}