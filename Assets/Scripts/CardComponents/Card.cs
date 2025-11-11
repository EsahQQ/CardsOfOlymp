using UnityEngine;

namespace CardComponents
{
    public class Card : MonoBehaviour
    {
        public CardData CardData { get; private set; }
        public CardVisual Visual { get; private set; }
        public CardDragger Dragger { get; private set; }
        
        private void Awake()
        {
            Visual = GetComponent<CardVisual>();
            Dragger = GetComponent<CardDragger>();
        }

        public void Init(CardData data)
        {
            CardData = data;
            Visual.Init(data);
        }
    }
}