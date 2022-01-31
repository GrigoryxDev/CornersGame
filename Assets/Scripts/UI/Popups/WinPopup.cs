using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI.Popups
{
    public class WinPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI playerTurnText;

        public void SetWinText(string playerName, int playerWinTurns)
        {
            playerTurnText.text = $"{playerName} won in {playerWinTurns} moves";
        }
    }
}