using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Assets.Scripts.Game.GameBoard
{
    public class PlayerChip : BaseBoardElement
    {
        [SerializeField] private RectTransform rectTransform;
       

        private Tween tween;

        public void Select()
        {
            var color = elementImage.color;
            color.a = .65f;
            elementImage.color = color;
        }

        public void Deselect() => elementImage.color = Color.white;

        public void ChangePosition(Vector2 newPosition, float moveTime)
        {
            tween?.Kill();
            tween = rectTransform.DOAnchorPos(newPosition, moveTime).SetEase(Ease.InSine);
        }
    }
}