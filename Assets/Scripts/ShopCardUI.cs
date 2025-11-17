using CardComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private CardVisual cardVisual; 
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    private CardData _cardData;
    private ShopManager _shopManager;
    
    public void Init(CardData data, ShopManager manager)
    {
        _cardData = data;
        _shopManager = manager;
        
        cardVisual.Init(_cardData);
        priceText.text = $"{_cardData.price} G";
        
        buyButton.onClick.AddListener(OnBuyButtonPressed);
    }

    private void OnBuyButtonPressed()
    {
        _shopManager.TryBuyCard(_cardData, this);
    }
    
    public void SetAsPurchased()
    {
        buyButton.interactable = false;
        priceText.text = "Куплено";
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(OnBuyButtonPressed);
    }
}