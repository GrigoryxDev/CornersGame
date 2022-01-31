using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game.States
{
    public abstract class BaseState : MonoBehaviour
    {
        protected StateMachine stateMachine;

        [Inject]
        private void Constructor(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public abstract States GetStateType { get; }

        public abstract void Enable();

        public abstract void Disable();
    }
}