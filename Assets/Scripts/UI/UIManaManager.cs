using System;
using BattleComponents;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManaManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI manaText;
        
        private LogicalMana _logicalMana;
        
        public void LinkToManaModel(LogicalMana logicalMana)
        {
            _logicalMana =  logicalMana;
            UpdateManaText();
            
            _logicalMana.OnManaChanged += OnManaChanged;
        }

        private void OnManaChanged(object sender, EventArgs e)
        {
            UpdateManaText();
        }

        private void UpdateManaText()
        {
            manaText.text = _logicalMana.CurrentMana.ToString();
        }
    }
}