using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
    public class DeckData : ScriptableObject
    {
        public List<CardData> cards;
    }
}