using System;
using System.Collections.Generic;
using Assets.Scripts.Game.GameBoard;

namespace Assets.Scripts.Game.Models
{
    public class CurrentGameModel
    {
        private PlayerModel[] players;
        private int currentPlayerIndex;
        public PlayerModel GetCurrentPlayerModel => players[currentPlayerIndex];

        public bool DiagonallyJump { get; private set; }
        public bool UpAndDownJump { get; private set; }

        public void InitGameData(PlayerModel[] newPlayers)
        {
            players = newPlayers;
            currentPlayerIndex = 0;
        }

        public void NextPlayerTurn()
        {
            if (currentPlayerIndex < players.Length - 1)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 0;
            }
            players[currentPlayerIndex].MakeTurn();
        }

        internal void SetJumpToggles(bool diagonaly, bool upAndDown)
        {
            DiagonallyJump = diagonaly;
            UpAndDownJump = upAndDown;
        }
    }
}