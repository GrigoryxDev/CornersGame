using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.GameBoard;
using Assets.Scripts.Game.Models;
using Assets.Scripts.Game.States;
using Assets.Scripts.PopupSpawnSystem;
using Assets.Scripts.UI.Popups;
using Assets.Scripts.UI.StateView;
using Zenject;

namespace Assets.Scripts.Game
{

    public class GameObserver
    {
        private CurrentGameModel currentGameModel;
        private GameDataGenerator gameDataGenerator;
        private Board gameBoard;
        private PopupFactory popupFactory;
        private StateMachine stateMachine;
        private GameStateView gameStateView;
        private BoardSaveModel boardSaveModel;

        public GameStatus CurrentGameStatus { get; private set; }

        [Inject]
        private void Constructor(GameDataGenerator gameDataGenerator, CurrentGameModel currentGameModel, Board gameBoard, PopupFactory popupFactory,
         StateMachine stateMachine, GameStateView gameStateView, UserInput userInput)
        {
            this.gameBoard = gameBoard;
            this.popupFactory = popupFactory;
            this.stateMachine = stateMachine;
            this.gameStateView = gameStateView;
            this.currentGameModel = currentGameModel;
            this.gameDataGenerator = gameDataGenerator;

            CurrentGameStatus = GameStatus.Inititalisation;

            userInput.Initialize();
            userInput.EventOnElementMoved += CheckFinish;
        }

        public void StartGame()
        {
            boardSaveModel = gameDataGenerator.GetSaveData();
            PrepareGameSpace();

            popupFactory.SpawnPopup<StartSettingsPopup>(PopupNames.StartSettingsPopup.ToString(),
            (startSettingsPopup) =>
            {
                startSettingsPopup.EventOnClose += () =>
                {
                    var toggleInfo = startSettingsPopup.GetTogglesInfo();
                    currentGameModel.InitGameData(gameDataGenerator.GetNewPlayers(boardSaveModel));//TODO: normal realisation
                    currentGameModel.SetJumpToggles(toggleInfo.diagonally, toggleInfo.upAndDown);

                    UpdateUITexts();

                    CurrentGameStatus = GameStatus.Active;
                };
            });
        }
        public void ExtiToMenu() => stateMachine.ChangeState(StatesEnum.MainMenu);

        public void RestartGame()
        {
            CurrentGameStatus = GameStatus.Inititalisation;

            PrepareGameSpace();
            currentGameModel.InitGameData(gameDataGenerator.GetNewPlayers(boardSaveModel));

            UpdateUITexts();

            CurrentGameStatus = GameStatus.Active;
        }

        public void CheckFinish()
        {
            CurrentGameStatus = GameStatus.Inititalisation;

            if (currentGameModel.GetCurrentPlayerModel.IsPlayerWin())
            {
                GameFinished();
            }
            else
            {
                StartNextTurn();
            }
        }

        private void StartNextTurn()
        {
            currentGameModel.NextPlayerTurn();
            UpdateUITexts();

            CurrentGameStatus = GameStatus.Active;
        }

        private void GameFinished()
        {
            popupFactory.SpawnPopup<WinPopup>(PopupNames.WinPopup.ToString(),
            (winPopup) =>
             {
                 winPopup.EventOnClose += () => ExtiToMenu();

                 var playerModel = currentGameModel.GetCurrentPlayerModel;
                 winPopup.SetWinText(playerModel.Name, playerModel.Turns);
             });
        }

        private void PrepareGameSpace()
        {
            gameBoard.PrepareBoard(boardSaveModel);

            gameStateView.GetPlayerTurnsCounterText.text = string.Empty;
            gameStateView.GetPlayerTurnIndicator.Hide();
        }

        private void UpdateUITexts()
        {
            var playerModel = currentGameModel.GetCurrentPlayerModel;
            gameStateView.UpdatePlayerCounterText(playerModel.Name, playerModel.Turns);
            gameStateView.GetPlayerTurnIndicator.Show(playerModel.Name);
        }
    }
}