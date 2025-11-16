using UnityEngine;

namespace CardComponents
{
    public class Card : MonoBehaviour
    {
        public CardData CardData { get; private set; }
        private CardVisual _visual;
        private CardDragger _dragger;
        
        private void Awake()
        {
            _visual = GetComponent<CardVisual>();
            _dragger = GetComponent<CardDragger>();
        }

        public void Init(CardData data)
        {
            CardData = data;
            _visual.Init(data);
        }
    }
}