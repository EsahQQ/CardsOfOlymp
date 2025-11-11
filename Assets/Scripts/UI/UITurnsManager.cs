using System;
using BattleComponents;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UITurnsManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI turnsText;
        
        private LogicalTurns _logicalTurns;
        
        public void LinkToTurnsModel(LogicalTurns logicalTurns)
        {
            _logicalTurns =  logicalTurns;
            UpdateManaText();
            
            _logicalTurns.OnTurnsChanged += OnTurnsChanged;
        }

        private void OnTurnsChanged(object sender, EventArgs e)
        {
            UpdateManaText();
        }

        private void UpdateManaText()
        {
            turnsText.text = _logicalTurns.CurrentTurns.ToString();
        }
    }
}