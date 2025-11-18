using BattleComponents;
using EnemyComponents;
using UI;

namespace Core
{
    public class BattleContext
    {
        public LogicalHand PlayerHand { get; set; }
        public LogicalDeck PlayerDeck { get; set; }
        public LogicalDrop PlayerDrop { get; set; }
        public LogicalMana PlayerMana { get; set; }
        public LogicalTurns PlayerTurns { get; set; }
        
        public EnemyController Enemy { get; set; }
        
        
        public UIHandManager HandManager { get; set; }
        public UIDropManager DropManager { get; set; }
        public UIDeckManager DeckManager { get; set; }
    }
}