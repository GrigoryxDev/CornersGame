using UnityEngine;
using Assets.Scripts.Game.GameBoard;
using Zenject;
using Assets.Scripts.Game.Models;

namespace Assets.Scripts.Game
{
    public class GameElementsMover
    {
        private ChipHolder selectedChipHolder;

        private Board board;
        private CurrentGameModel currentGameModel;

        [Inject]
        private void Constructor(Board board, CurrentGameModel currentGameModel)
        {
            this.board = board;
            this.currentGameModel = currentGameModel;

        }

        public MoverResult TryMove(ChipHolder checkedChipHolder)
        {
            if (TrySelectCurrentHolder(checkedChipHolder))
            {
                return MoverResult.Select;
            }

            if (IsCanBeMoved(selectedChipHolder, checkedChipHolder))
            {
                var playerElement = selectedChipHolder.TakeChip();
                selectedChipHolder = null;

                checkedChipHolder.SetPlayerElement(playerElement);

                return MoverResult.Move;
            }
            else
            {
                if (selectedChipHolder)
                {
                    selectedChipHolder.GetPlayerElement.Deselect();
                }

                return MoverResult.Deselect;
            }
        }

        private bool IsCanBeMoved(ChipHolder current, ChipHolder target)
        {
            if (current == null)
            {
                return false;
            }

            bool isUpAndDownJump = currentGameModel.UpAndDownJump;
            bool isDiagonalyJump = currentGameModel.DiagonallyJump;

            return IsCanJumpUpAndDown(isUpAndDownJump, current, target) ||
            IsCanJumpDiagonally(isDiagonalyJump, current, target) ||
            IsOneStepMove(current, target);
        }

        private bool IsCanJumpUpAndDown(bool isUpAndDownJump, ChipHolder current, ChipHolder target)
        {
            if (isUpAndDownJump && target.IsEmpty)
            {
                var currentIndex = current.HolderIndex;
                var targetIndex = target.HolderIndex;

                if (currentIndex.x == targetIndex.x &&
                    Mathf.Abs(Mathf.Abs(currentIndex.y) - Mathf.Abs(targetIndex.y)) == 2)
                {
                    int checkY = (currentIndex.y + targetIndex.y) / 2;
                    var checkIndex = new Vector2Int(currentIndex.x, checkY);
                    if (board.IsContainsHolder(checkIndex, out ChipHolder chipHolder))
                    {
                        if (!chipHolder.IsEmpty)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsCanJumpDiagonally(bool isDiagonallyJump, ChipHolder current, ChipHolder target)
        {
            if (isDiagonallyJump && target.IsEmpty)
            {
                var currentIndex = current.HolderIndex;
                var targetIndex = target.HolderIndex;

                if (Mathf.Abs(Mathf.Abs(currentIndex.x) - Mathf.Abs(targetIndex.x)) == 2 &&
                    Mathf.Abs(Mathf.Abs(currentIndex.y) - Mathf.Abs(targetIndex.y)) == 2)
                {
                    Vector2Int checkIndex = (currentIndex + targetIndex) / 2;
                    if (board.IsContainsHolder(checkIndex, out ChipHolder chipHolder))
                    {
                        if (!chipHolder.IsEmpty)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsOneStepMove(ChipHolder current, ChipHolder target)
        {
            if (!target.IsEmpty)
            {
                return false;
            }

            bool diagonallyOneStep =
            Mathf.Abs(Mathf.Abs(current.HolderIndex.x) - Mathf.Abs(target.HolderIndex.x)) == 1 &&
            Mathf.Abs(Mathf.Abs(current.HolderIndex.y) - Mathf.Abs(target.HolderIndex.y)) == 1;

            bool crossOneStep = Vector2Int.Distance(current.HolderIndex, target.HolderIndex) == 1;

            bool result = diagonallyOneStep || crossOneStep;

            return result;
        }

        private bool TrySelectCurrentHolder(ChipHolder checkedHolder)
        {
            if (!checkedHolder.IsEmpty &&
            currentGameModel.GetCurrentPlayerModel.IsPlayerChip(checkedHolder.GetPlayerElement))
            {
                if (selectedChipHolder)
                {
                    selectedChipHolder.GetPlayerElement.Deselect();
                }

                selectedChipHolder = checkedHolder;
                selectedChipHolder.GetPlayerElement.Select();
                return true;
            }

            return false;
        }
    }
}