using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.Models;
using Assets.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game.GameBoard
{
    public class Board : MonoBehaviour
    {
        private GameSettingsSO gameSettingsSO;
        private GameFactory gameFactory;

        private readonly Dictionary<Vector2Int, ChipHolder> chipHolders = new Dictionary<Vector2Int, ChipHolder>();

        [Inject]
        private void Constructor(GameSettingsSO gameSettingsSO, GameFactory gameFactory)
        {
            this.gameSettingsSO = gameSettingsSO;
            this.gameFactory = gameFactory;
        }

        public void PrepareBoard()
        {
            ClearBoard();

            var gameBoardModel = GetSaveData();

            var elementSize = CalcElementSize(gameBoardModel.boardSize);

            var startRectPosition = Vector2.zero + new Vector2(elementSize.x / 2, -elementSize.y / 2);
            var currentRectPosition = startRectPosition;

            var blackHolderSprite = Resources.Load<Sprite>("Sprites/Black");
            var whiteHolderSprite = Resources.Load<Sprite>("Sprites/White");

            var nextSprite = whiteHolderSprite;
            for (var y = 0; y < gameBoardModel.boardSize.y; y++)
            {
                for (var x = 0; x < gameBoardModel.boardSize.x; x++)
                {
                    var index = new Vector2Int(x, y);

                    ChipHolder chipHolder = gameFactory.SpawnChipHolder(transform);
                    chipHolder.SetNewPositionAndSize(elementSize, currentRectPosition);
                    chipHolder.Init(index, nextSprite);

                    CheckPlayerChipSpawn(index, gameBoardModel, elementSize, chipHolder);

                    chipHolders.Add(index, chipHolder);
                    currentRectPosition.x += elementSize.x;
                    if (x == gameBoardModel.boardSize.x - 1)
                    {
                        continue;
                    }
                    else if (nextSprite == blackHolderSprite)
                    {
                        nextSprite = whiteHolderSprite;
                    }
                    else
                    {
                        nextSprite = blackHolderSprite;
                    }
                }

                currentRectPosition.x = startRectPosition.x;
                currentRectPosition.y -= elementSize.y;
            }
        }

        private void ClearBoard()
        {
            foreach (var item in chipHolders)
            {
                Destroy(item.Value.gameObject);
            }
            chipHolders.Clear();
        }

        private BoardSaveModel GetSaveData()
        {
            BoardSaveModel gameBoardModel;
            gameBoardModel = JsonWrapper.GetGameBoardModel();
            if (gameBoardModel == null)
            {
                gameBoardModel = gameSettingsSO.PrepareJsonBoardModel();
            }
            return gameBoardModel;
        }

        private Vector2 CalcElementSize(Vector2 boardSize)
        {
            var rectTransform = GetComponent<RectTransform>();
            var panelSize = rectTransform.rect.size;
            float elementSizeX = panelSize.x / boardSize.x;
            float elementSizeY = panelSize.y / boardSize.y;
            Vector2 elementSize = new Vector2(elementSizeX, elementSizeY);
            return elementSize;
        }

        private void CheckPlayerChipSpawn(Vector2Int index, BoardSaveModel gameBoardModel, Vector2 elementSize, ChipHolder holder)
        {
            Color color;
            if (gameBoardModel.firstPlayerStartChipPositions.Contains(index))
            {
                color = gameSettingsSO.GetFirstPlayerColor;
            }
            else if (gameBoardModel.secondPlayerStartChipPositions.Contains(index))
            {
                color = gameSettingsSO.GetSecondPlayerColor;
            }
            else
            {
                return;
            }

            holder.SetColor(color);

            PlayerChip playerChip = gameFactory.SpawnChip(holder.transform);
            playerChip.SetNewPositionAndSize(elementSize, Vector2.zero);
            playerChip.SetColor(color);
        }

        //TODO: set chips to player and compare by saved target lists
        //MB bind player model
        //Set chips to player
        //Set player target holders
        //User input
        //Check swaps from up and down and diagonal
        //Restart should be only reset
    }
}