using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public string description;
        public Sprite image;
    }
}
