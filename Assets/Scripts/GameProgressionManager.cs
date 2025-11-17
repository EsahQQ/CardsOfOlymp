using System.Collections.Generic;
using CardComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressionManager : MonoBehaviour
{
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
        
        Debug.Log("Генерируем стартовую колоду..."); 
    }

    public void StartNextBattle()
    {
        Debug.Log($"Начинаем бой на этаже {CurrentLevel}");

        SceneManager.LoadScene("BattleScene"); 
    }

    public void BattleWon()
    {
        Debug.Log("Победа в бою!");
        CurrentLevel++;
        PlayerGold += 100; // Награда за победу
        
        SceneManager.LoadScene("ShopScene");
    }

    public void BattleLost()
    {
        Debug.Log("Поражение!");

        SceneManager.LoadScene("MainMenuScene");
    }
}