using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.GameBoard
{
    public class ChipHolder : BaseBoardElement
    {
        public Vector2Int HolderIndex { get; private set; }
        public PlayerChip PlayerChip { get; private set; }

        public bool IsEmpty => PlayerChip == null;

        public void Init(Vector2Int holderIndex, Sprite holderSprite)
        {
            HolderIndex = holderIndex;
            elementImage.sprite = holderSprite;
        }

        public void SetChip(PlayerChip newChip) => PlayerChip = newChip;
    }
}