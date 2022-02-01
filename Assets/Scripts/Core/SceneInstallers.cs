using Zenject;
using UnityEngine;
using Assets.Scripts.PopupSpawnSystem;
using Assets.Scripts.Game;
using Assets.Scripts.Game.States;
using Assets.Scripts.Game.GameBoard;
using Assets.Scripts.UI.StateView;

namespace Assets.Scripts.Core
{
    public class SceneInstallers : MonoInstaller
    {
        [SerializeField] private StateMachine stateMachine;


        [SerializeField, Header("UI elements")]
        private MatePanel matePanel;
        [SerializeField] private PopupFactory popupFactory;
        [SerializeField] private GameStateView gameStateView;


        [SerializeField, Header("Game elements")]
        private GameFactory gameFactory;
        [SerializeField] private GameObserver gameObserver;
        [SerializeField] private Board gameBoard;

        public override void InstallBindings()
        {
            BindSingleNonLazyFromInstance(stateMachine);

            BindSingleNonLazyFromInstance(matePanel);
            BindSingleNonLazyFromInstance(popupFactory);
            BindSingleNonLazyFromInstance(gameStateView);

            BindSingleNonLazyFromInstance(gameFactory);
            BindSingleNonLazyFromInstance(gameObserver);
            BindSingleNonLazyFromInstance(gameBoard);
        }

        private void BindSingleNonLazyFromInstance<T>(T instance)
        {
            Container.Bind<T>()
            .FromInstance(instance)
            .AsSingle().NonLazy();
        }

    }
}