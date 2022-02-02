using System.Collections.Generic;
using Assets.Scripts.Game.Models;
using Assets.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "ScriptableObject/GameSettingsSO", order = 0)]
    public class GameSettingsSO : ScriptableObjectInstaller<GameSettingsSO>
    {
        [SerializeField] private float moveStateAnimationTime = .25f;
        [SerializeField] private float fadeStateTime = .4f;

        [SerializeField, Space()] private Color firstPlayerColor = Color.green;
        [SerializeField] private Color secondPlayerColor = Color.red;
        [SerializeField] private float moveChipAnimationTime = .25f;


        //TODO: With editor we can set any board size and player chipsHouse
        //Include rectangular form 
        private int boardSize = 8;
        private int playerChipsHouse = 3;

        public float GetMoveChipTime => moveChipAnimationTime;
        public Color GetFirstPlayerColor => firstPlayerColor;
        public Color GetSecondPlayerColor => secondPlayerColor;

        public float GetMoveStateAnimationTime => moveStateAnimationTime;
        public float GetFadeStateTime => fadeStateTime;


        public BoardSaveModel PrepareJsonBoardModel()
        {
            var gameBoardModel = new BoardSaveModel
            {
                boardSize = new Vector2Int(boardSize, boardSize)
            };

            //First player always start from left down side
            var firstPlayerStartPosition = new Vector2Int(0, boardSize);

            for (var y = firstPlayerStartPosition.y - 1; y >= firstPlayerStartPosition.y - playerChipsHouse; y--)
            {
                for (var x = firstPlayerStartPosition.x; x < playerChipsHouse; x++)
                {
                    var index = new Vector2Int(x, y);
                    gameBoardModel.firstPlayerStartPositions.Add(index);
                }
            }

            //Second player always start from top right side
            var secondPlayerStartPosition = new Vector2Int(boardSize, 0);

            for (var y = secondPlayerStartPosition.y; y < playerChipsHouse; y++)
            {
                for (var x = secondPlayerStartPosition.x - 1; x >= secondPlayerStartPosition.x - playerChipsHouse; x--)
                {
                    var index = new Vector2Int(x, y);
                    gameBoardModel.secondPlayerStartPositions.Add(index);
                }
            }

            JsonWrapper.SaveGameBoardModel(gameBoardModel);
            return gameBoardModel;
        }

        public override void InstallBindings()
        {
            Container.Bind<GameSettingsSO>()
            .FromInstance(this)
            .AsSingle().NonLazy();
        }
    }
}