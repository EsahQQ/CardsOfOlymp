using UnityEngine;

namespace CardComponents
{
    // Этот класс теперь - просто носитель данных и точка входа.
    public class Card : MonoBehaviour
    {
        public CardData CardData { get; private set; }

        // Ссылки для удобства доступа из других систем
        public CardVisual Visual { get; private set; }
        public CardDragger Dragger { get; private set; }
        
        private void Awake()
        {
            // Собираем ссылки на свои же компоненты
            Visual = GetComponent<CardVisual>();
            Dragger = GetComponent<CardDragger>();
        }

        public void Init(CardData data)
        {
            CardData = data;
            // Передаем данные визуальной части для отрисовки
            Visual.Init(data);
        }
    }
}