using System;

namespace BattleComponents
{
    public class LogicalMana
    {
        public int CurrentMana { get; private set; }
        
        public event EventHandler OnManaChanged;

        public LogicalMana(int startMana)
        {
            CurrentMana = startMana;
        }

        public void AddMana(int manaCost)
        {
            CurrentMana += manaCost;
            OnManaChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}