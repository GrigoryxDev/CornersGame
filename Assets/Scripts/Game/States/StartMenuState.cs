using Assets.Scripts.UI.StateView;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Game.States
{
    public class StartMenuState : BaseState
    {
        [SerializeField] private StartMenuStateView mainMenuStateView;
        [SerializeField] private Vector2 enableButtonPosition;
        [SerializeField] private Vector2 disableButtonPosition;

        private Sequence sequence;
        public override StatesEnum GetStateType => StatesEnum.MainMenu;

        private void Start()
        {
            mainMenuStateView.GeStartGameButton.onClick.AddListener(() =>
            {
                stateMachine.ChangeState(StatesEnum.Game);
            });
        }

        public override void Enable()
        {
            gameObject.SetActive(true);

            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(mainMenuStateView.GeStateCanvasGroup.DOFade(1, GetFadeTime))
            .Join(mainMenuStateView.GeStartGameButton.GetComponent<RectTransform>()
            .DOAnchorPos(enableButtonPosition, GetMoveAnimationTime))
            .SetEase(Ease.InSine);
        }

        public override void Disable()
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(mainMenuStateView.GeStateCanvasGroup.DOFade(0, GetFadeTime))
            .Join(mainMenuStateView.GeStartGameButton.GetComponent<RectTransform>()
            .DOAnchorPos(disableButtonPosition, GetMoveAnimationTime))
            .SetEase(Ease.InSine)
            .OnComplete(() => gameObject.SetActive(false));
        }
    }
}