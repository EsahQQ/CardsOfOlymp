using System.Collections.Generic;
using CardComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Настройки магазина")]
    [SerializeField] private CardPool shopCardPool;
    [SerializeField] private int cardsOnSaleCount = 3;

    [Header("Ссылки на UI")]
    [SerializeField] private Transform[] cardSlotsParents;
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private TextMeshProUGUI playerGoldText;
    [SerializeField] private Button continueButton;

    private List<CardData> _cardsForSale = new List<CardData>();

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonPressed);
        
        UpdatePlayerGoldUI();
        GenerateWares();
    }

    private void GenerateWares()
    {
        foreach (var cardSlotsParent in cardSlotsParents)
        {
            foreach (Transform child in cardSlotsParent)
            {
                Destroy(child.gameObject);
            }
            _cardsForSale.Clear();
        
            List<CardData> availableCards = new List<CardData>(shopCardPool.allCards);
            for (int i = 0; i < cardsOnSaleCount; i++)
            {
                if (availableCards.Count == 0) break;

                int randomIndex = Random.Range(0, availableCards.Count);
                CardData randomCard = availableCards[randomIndex];
            
                _cardsForSale.Add(randomCard);
                availableCards.RemoveAt(randomIndex); 
            }
        
            foreach (var cardData in _cardsForSale)
            {
                GameObject shopCardObject = Instantiate(shopCardPrefab, cardSlotsParent);

                shopCardObject.GetComponent<ShopCardUI>().Init(cardData, this);
            }
        }
    }
    
    public bool TryBuyCard(CardData cardToBuy, ShopCardUI cardUI)
    {
        var progression = GameProgressionManager.Instance;
        
        if (progression.PlayerGold >= cardToBuy.price)
        {
            progression.ChangeGold(-cardToBuy.price);
            
            progression.PlayerDeck.Add(cardToBuy);
            
            Debug.Log($"Игрок купил карту {cardToBuy.cardName}!");
            
            UpdatePlayerGoldUI();
            
            cardUI.SetAsPurchased();
            
            return true;
        }
        else
        {
            Debug.Log("Недостаточно золота!");
            return false;
        }
    }

    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = GameProgressionManager.Instance.PlayerGold.ToString();
    }

    private void OnContinueButtonPressed()
    {
        GameProgressionManager.Instance.StartNextBattle();
    }
}