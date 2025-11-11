using System;
using UnityEngine;

namespace BattleComponents
{
    public class LogicalTurns
    {
        public int CurrentTurns { get; private set; }
        
        public event EventHandler OnTurnsChanged;

        public LogicalTurns(int startTurns)
        {
            CurrentTurns = startTurns;
        }

        public void AddMana(int turns)
        {
            CurrentTurns += turns;
            OnTurnsChanged?.Invoke(this, EventArgs.Empty);
            if (CurrentTurns <= 0)
            {
                Debug.Log("Game over! Not enough turns!");
            }
        }
    }
}