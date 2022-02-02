using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.UI.StateView;
using Zenject;

namespace Assets.Scripts.Game.States
{
    public class GameState : BaseState
    {
        [SerializeField] private GameStateView gameStateView;

        private Tween tween;
        private GameObserver gameObserver;
        public override StatesEnum GetStateType => StatesEnum.Game;

        [Inject]
        private void Constructor(GameObserver gameObserver)
        {
            this.gameObserver = gameObserver;

            gameStateView.GetRestartButton.onClick.AddListener(() =>
           {
               gameObserver.RestartGame();
           });
        }

        public override void Enable()
        {
            gameObject.SetActive(true);

            tween?.Kill();
            tween = gameStateView.GeStateCanvasGroup.DOFade(1, GetFadeTime)
             .SetEase(Ease.InSine);

            gameObserver.StartGame();
        }

        public override void Disable(bool immediately = false)
        {
            tween?.Kill();
            if (immediately)
            {
                gameObject.SetActive(false);
            }
            else
            {
                tween = gameStateView.GeStateCanvasGroup.DOFade(0, GetFadeTime)
                .SetEase(Ease.InSine)
                .OnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}