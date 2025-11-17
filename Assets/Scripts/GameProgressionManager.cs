using System.Collections.Generic;
using CardComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressionManager : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private CardPool initialCardPool;
    [SerializeField] private int startingDeckSize = 8;
    
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

        SceneManager.LoadScene("Main"); 
    }

    public void BattleWon()
    {
        Debug.Log("Победа в бою!");
        CurrentLevel++;
        PlayerGold += 100; 
        
        SceneManager.LoadScene("ShopScene");
    }

    public void BattleLost()
    {
        Debug.Log("Поражение!");

        SceneManager.LoadScene("MainMenu");
    }
}