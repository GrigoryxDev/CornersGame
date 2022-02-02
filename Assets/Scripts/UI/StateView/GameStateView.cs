using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.StateView
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI playerTurnsCounterText;
        [SerializeField] private CanvasGroup stateGroup;
        [SerializeField] private PlayerTurnIndicator playerTurnIndicator;

        public PlayerTurnIndicator GetPlayerTurnIndicator => playerTurnIndicator;
        public CanvasGroup GeStateCanvasGroup => stateGroup;
        public Button GetRestartButton => restartButton;
        public Button GetExitButton => exitButton;
        public TextMeshProUGUI GetPlayerTurnsCounterText => playerTurnsCounterText;

        public void UpdatePlayerCounterText(string playerName, int playerTurn)
        {
            playerTurnsCounterText.text = $"{playerName} turn: {playerTurn}";
        }
    }
}