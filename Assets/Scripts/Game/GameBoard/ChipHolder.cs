using UnityEngine;

namespace Assets.Scripts.Game.GameBoard
{
    public class ChipHolder : BaseBoardElement
    {
        public Vector2Int HolderIndex { get; private set; }
        public IPlayerElement GetPlayerElement { get; private set; }

        public bool IsEmpty => GetPlayerElement == null;
        public Vector2 GetAnchoredPosition => rectTransform.anchoredPosition;
        public void Init(Vector2Int holderIndex, Sprite holderSprite, Color color, IPlayerElement newPlayerElement)
        {
            HolderIndex = holderIndex;
            elementImage.sprite = holderSprite;
            GetPlayerElement = newPlayerElement;
            SetColor(color);
        }

        public void SetPlayerElement(IPlayerElement newChip)
        {
            GetPlayerElement = newChip;

            GetPlayerElement.Deselect();
            GetPlayerElement.GetTransform.SetAsLastSibling();
            GetPlayerElement.ChangePosition(rectTransform.anchoredPosition);
        }

        public IPlayerElement TakeChip()
        {
            var playerElement = GetPlayerElement;
            GetPlayerElement = null;
            return playerElement;
        }
    }
}