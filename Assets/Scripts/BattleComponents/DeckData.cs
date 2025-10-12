using System.Collections.Generic;
using CardComponents;
using UnityEngine;

namespace BattleComponents
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
    public class DeckData : ScriptableObject
    {
        public List<CardData> cards;
    }
}