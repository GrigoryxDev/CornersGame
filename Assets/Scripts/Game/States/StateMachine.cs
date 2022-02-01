using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PopupSpawnSystem;
using UnityEngine;

namespace Assets.Scripts.Game.States
{
    public class StateMachine : MonoBehaviour
    {
        private readonly Dictionary<StatesEnum, BaseState> states = new Dictionary<StatesEnum, BaseState>();
        public BaseState CurrentState { get; private set; }

        private void Start()
        {
            var statesArray = GetComponentsInChildren<BaseState>(true);

            for (int i = 0; i < statesArray.Length; i++)
            {
                var state = statesArray[i];
                states.Add(state.GetStateType, state);
            }

            InitStates(StatesEnum.MainMenu);
        }

        public void ChangeState(StatesEnum state)
        {
            CurrentState.Disable();

            CurrentState = states[state];

            CurrentState.Enable();
        }

        private void InitStates(StatesEnum startState)
        {
            foreach (var item in states)
            {
                if (item.Key == startState)
                {
                    CurrentState = item.Value;

                    CurrentState.Enable();
                }
                else
                {
                    item.Value.Disable();
                }
            }
        }
    }
}