using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.GameBoard;
using Assets.Scripts.Game.Models;
using Assets.Scripts.Game.States;
using Assets.Scripts.PopupSpawnSystem;
using Assets.Scripts.UI.Popups;
using Assets.Scripts.UI.StateView;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class GameObserver : MonoBehaviour
    {
        private GameModel gameModel;
        private Board gameBoard;
        private PopupFactory popupFactory;
        private StateMachine stateMachine;
        private GameStateView gameStateView;

        [Inject]
        private void Constructor(Board gameBoard, PopupFactory popupFactory,
         StateMachine stateMachine, GameStateView gameStateView)
        {
            this.gameBoard = gameBoard;
            this.popupFactory = popupFactory;
            this.stateMachine = stateMachine;
            this.gameStateView = gameStateView;
        }

        public void StartGame()
        {
            gameBoard.PrepareBoard();

            popupFactory.SpawnPopup<StartSettingsPopup>(PopupNames.StartSettingsPopup.ToString(),
            (startSettingsPopup) =>
            {
                startSettingsPopup.EventOnClose += () =>
                {
                    var toggleInfo = startSettingsPopup.GetTogglesInfo();
                    PlayerModel[] players = new PlayerModel[2]
                               {
                                     new PlayerModel(0,"Player1"),
                                     new PlayerModel(0,"Player2"),
                               };

                    gameModel = new GameModel(players);

                    //TODO: observe player turns and show ui
                };
            });
        }

        public void CheckFinish()
        {
            if (true)
            {
                StartNextTurn();
            }
            else
            {
                GameFinished();
            }
        }

        private void StartNextTurn()
        {
            gameModel.NextPlayerTurn();
            var playerModel = gameModel.GetCurrentPlayerModel;
            gameStateView.UpdatePlayerCounterText(playerModel.Name, playerModel.Turns);
        }

        private void GameFinished()
        {
            popupFactory.SpawnPopup<WinPopup>(PopupNames.WinPopup.ToString(),
            (winPopup) =>
             {
                 winPopup.EventOnClose += () => stateMachine.ChangeState(StatesEnum.MainMenu);

                 var playerModel = gameModel.GetCurrentPlayerModel;
                 winPopup.SetWinText(playerModel.Name, playerModel.Turns);
             });
        }
    }
}