using Zenject;
using UnityEngine;
using Assets.Scripts.PopupSpawnSystem;
using Assets.Scripts.Game;
using Assets.Scripts.Game.States;

namespace Assets.Scripts.Core
{
    public class SceneInstallers : MonoInstaller
    {
        [SerializeField] private StateMachine stateMachine;


        [SerializeField, Header("UI elements")]
        private MatePanel matePanel;
        [SerializeField] private PopupFactory popupFactory;


        [SerializeField, Header("Game elements")]
        private GameFactory gameFactory;

        public override void InstallBindings()
        {
            BindSingleNonLazyFromInstance(matePanel);
            BindSingleNonLazyFromInstance(popupFactory);
            BindSingleNonLazyFromInstance(gameFactory);
            BindSingleNonLazyFromInstance(stateMachine);
        }

        private void BindSingleNonLazyFromInstance<T>(T instance)
        {
            Container.Bind<T>()
            .FromInstance(instance)
            .AsSingle().NonLazy();
        }

    }
}