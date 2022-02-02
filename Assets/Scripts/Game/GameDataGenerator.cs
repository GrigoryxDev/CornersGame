using Assets.Scripts.Game.GameBoard;
using Assets.Scripts.Game.Models;
using Assets.Scripts.Utilities;
using Zenject;

namespace Assets.Scripts.Game
{
    public class GameDataGenerator
    {
        private Board gameBoard;
        private GameSettingsSO gameSettingsSO;

        [Inject]
        private void Constructor(Board gameBoard, GameSettingsSO gameSettingsSO)
        {
            this.gameBoard = gameBoard;
            this.gameSettingsSO = gameSettingsSO;
        }

        public BoardSaveModel GetSaveData()
        {
            var gameBoardModel = JsonWrapper.GetGameBoardModel();
            if (gameBoardModel == null)
            {
                gameBoardModel = gameSettingsSO.PrepareJsonBoardModel();
            }

            return gameBoardModel;
        }

        public PlayerModel[] GetNewPlayers(BoardSaveModel boardSaveModel)
        {
            PlayerModel[] players = new PlayerModel[2];
            var firstPlayerLists = gameBoard.GetPlayerElements(boardSaveModel.firstPlayerStartPositions,
            boardSaveModel.secondPlayerStartPositions);

            var firstPlayer = new PlayerModel("Player 1", firstPlayerLists.targetHolders, firstPlayerLists.playerChips);
            players[0] = firstPlayer;

            var secondPlayerLists = gameBoard.GetPlayerElements(boardSaveModel.secondPlayerStartPositions,
            boardSaveModel.firstPlayerStartPositions);

            var secondPlayer = new PlayerModel("Player 2", secondPlayerLists.targetHolders, secondPlayerLists.playerChips);
            players[1] = secondPlayer;

            return players;
        }
    }
}