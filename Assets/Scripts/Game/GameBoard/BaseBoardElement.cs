using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.GameBoard
{
    public abstract class BaseBoardElement : MonoBehaviour
    {
        [SerializeField] protected Image elementImage;

        public void SetColor(Color playerColor)
        {
            elementImage.color = playerColor;
        }

        public void SetNewPositionAndSize(Vector2 size, Vector2 position)
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
            rectTransform.anchoredPosition = position;
        }
    }
}