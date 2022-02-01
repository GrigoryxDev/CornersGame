using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.StateView
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI playerTurnsCounterText;
        [SerializeField] private CanvasGroup stateGroup;

        public CanvasGroup GeStateCanvasGroup => stateGroup;
        public Button GetRestartButton => restartButton;

        public void UpdatePlayerCounterText(string playerName, int playerTurn)
        {
            playerTurnsCounterText.text = $"{playerName} turn: {playerTurn}";
        }
    }
}