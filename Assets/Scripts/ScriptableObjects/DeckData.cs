using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
    public class DeckData : ScriptableObject
    {
        public List<CardData> cards;
    }
}