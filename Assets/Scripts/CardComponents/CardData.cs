using Abilities;
using UnityEngine;

namespace CardComponents
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public string description;
        public Sprite image;
        public int manaCost;
        public Ability ability;
        public int price;
    }
}
