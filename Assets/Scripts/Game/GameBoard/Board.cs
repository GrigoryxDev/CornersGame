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

        public void PrepareBoard(BoardSaveModel gameBoardModel)
        {
            ClearBoard();

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

                    InitChipHolder(index, nextSprite, gameBoardModel, elementSize, chipHolder);

                    currentRectPosition.x += elementSize.x;

                    if (x == gameBoardModel.boardSize.x - 1)
                    {
                        continue;
                    }
                    else
                    {
                        UpdateNextSprite(ref nextSprite, blackHolderSprite, whiteHolderSprite);
                    }
                }

                currentRectPosition.x = startRectPosition.x;
                currentRectPosition.y -= elementSize.y;
            }
        }

        public (List<ChipHolder> targetHolders, List<IPlayerElement> playerChips) GetPlayerElements(List<Vector2Int> playerElements, List<Vector2Int> opponentElements)
        {
            var targetHolders = new List<ChipHolder>();
            foreach (var item in opponentElements)
            {
                targetHolders.Add(chipHolders[item]);
            }
            var playerChips = new List<IPlayerElement>();
            foreach (var item in playerElements)
            {
                playerChips.Add(chipHolders[item].GetPlayerElement);
            }

            return (targetHolders, playerChips);
        }

        public bool IsContainsHolder(Vector2Int index, out ChipHolder chipHolder)
        {
            chipHolder = null;

            if (chipHolders.ContainsKey(index))
            {
                chipHolder = chipHolders[index];
                return true;
            }

            return false;
        }

        private void UpdateNextSprite(ref Sprite nextSprite, Sprite blackHolderSprite, Sprite whiteHolderSprite)
        {
            if (nextSprite == blackHolderSprite)
            {
                nextSprite = whiteHolderSprite;
            }
            else
            {
                nextSprite = blackHolderSprite;
            }
        }
        private void InitChipHolder(Vector2Int index, Sprite nextSprite, BoardSaveModel gameBoardModel, Vector2 elementSize, ChipHolder holder)
        {
            Color color;
            bool spawnPlayerElement;
            if (gameBoardModel.firstPlayerStartPositions.Contains(index))
            {
                spawnPlayerElement = true;
                color = gameSettingsSO.GetFirstPlayerColor;
            }
            else if (gameBoardModel.secondPlayerStartPositions.Contains(index))
            {
                spawnPlayerElement = true;
                color = gameSettingsSO.GetSecondPlayerColor;
            }
            else
            {
                spawnPlayerElement = false;
                color = Color.white;
            }

            IPlayerElement playerElement = null;
            if (spawnPlayerElement)
            {
                playerElement = gameFactory.SpawnPlayerElement(transform);
                var baseBoardElement = playerElement.GetTransform.GetComponent<BaseBoardElement>();
                baseBoardElement.SetNewPositionAndSize(elementSize, holder.GetAnchoredPosition);
                baseBoardElement.SetColor(color);
            }

            holder.Init(index, nextSprite, color, playerElement);
            chipHolders.Add(index, holder);
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

        private void ClearBoard()
        {
            foreach (var item in chipHolders)
            {
                Destroy(item.Value.gameObject);
            }
            chipHolders.Clear();
        }
    }
}