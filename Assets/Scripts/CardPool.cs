using System.Collections.Generic;
using CardComponents;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Pool", menuName = "Card Pool")]
public class CardPool : ScriptableObject
{
    public List<CardData> allCards;
}