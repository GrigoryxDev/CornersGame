using Zenject;
using UnityEngine;
using Assets.Scripts.PopupSpawnSystem;
using Assets.Scripts.Game;
using Assets.Scripts.Game.States;
using Assets.Scripts.Game.GameBoard;
using Assets.Scripts.UI.StateView;
using Assets.Scripts.Game.Models;

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
        [SerializeField] private Board gameBoard;

        public override void InstallBindings()
        {
            //Core
            BindSingleNonLazyFromInstance(stateMachine);
            BindNewClassAsSingleNonLazy<GameDataGenerator>();
            BindNewClassAsSingleNonLazy<UserInput>();

            //UI
            BindSingleNonLazyFromInstance(matePanel);
            BindSingleNonLazyFromInstance(popupFactory);
            BindSingleNonLazyFromInstance(gameStateView);

            //Game
            BindSingleNonLazyFromInstance(gameFactory);
            BindSingleNonLazyFromInstance(gameBoard);
            BindNewClassAsSingleNonLazy<GameObserver>();
            BindNewClassAsSingleNonLazy<GameElementsMover>();

            BindNewClassAsSingleNonLazy<CurrentGameModel>();
        }

        private void BindNewClassAsSingleNonLazy<T>() => Container.Bind<T>().AsSingle().NonLazy();
        private void BindSingleNonLazyFromInstance<T>(T instance)
        {
            Container.Bind<T>()
            .FromInstance(instance)
            .AsSingle().NonLazy();
        }

    }
}