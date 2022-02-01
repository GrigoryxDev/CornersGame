using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game.States
{
    public abstract class BaseState : MonoBehaviour
    {
        protected StateMachine stateMachine;
        protected float GetMoveAnimationTime { get; private set; }
        protected float GetFadeTime { get; private set; }

        [Inject]
        private void Constructor(StateMachine stateMachine, GameSettingsSO gameSettingsSO)
        {
            this.stateMachine = stateMachine;
            GetMoveAnimationTime = gameSettingsSO.GetMoveStateAnimationTime;
            GetFadeTime = gameSettingsSO.GetFadeStateTime;
        }

        public abstract StatesEnum GetStateType { get; }

        public abstract void Enable();

        public abstract void Disable();
    }
}