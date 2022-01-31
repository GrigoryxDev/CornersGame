using Assets.Scripts.UI.StateView;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Game.States
{
    public class StartMenuState : BaseState
    {
        [SerializeField] private MainMenuStateView mainMenuStateView;

        public override States GetStateType => States.MainMenu;

        private void Start()
        {
            mainMenuStateView.GeStartGameButton.onClick.AddListener(() =>
            {
                stateMachine.ChangeState(States.Game);
            });
        }

        public override void Enable()
        {
            gameObject.SetActive(true);
        }

        public override void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}