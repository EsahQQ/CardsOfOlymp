using CardComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardComponents
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI manaCost;
        [SerializeField] private Image image;

        public void Init(CardData cardData)
        {
            cardName.text = cardData.cardName;
            description.text = cardData.ability.AbilityDescription;
            manaCost.text = cardData.manaCost.ToString();
            image.sprite = cardData.image;
        }
    }
}
