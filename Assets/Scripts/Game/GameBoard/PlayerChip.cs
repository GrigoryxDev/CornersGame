using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.Game.GameBoard
{
    public class PlayerChip : BaseBoardElement, IPlayerElement
    {
        private float moveTime;
        private Tween tween;

        public Transform GetTransform => transform;

        [Inject]
        private void Constructor(GameSettingsSO gameSettingsSO)
        {
            moveTime = gameSettingsSO.GetMoveChipTime;
        }

        public void Select() => ChangeImageAlpha(.65f);

        public void Deselect() => ChangeImageAlpha(1f);

        public void ChangePosition(Vector2 newPosition)
        {
            tween?.Kill();
            tween = rectTransform.DOAnchorPos(newPosition, moveTime).SetEase(Ease.InSine);
        }

        private void ChangeImageAlpha(float newValue)
        {
            var color = elementImage.color;
            color.a = newValue;
            elementImage.color = color;
        }
    }
}