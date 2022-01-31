using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PopupSpawnSystem;
using UnityEngine;

namespace Assets.Scripts.Game.States
{
    public class StateMachine : MonoBehaviour
    {
        private readonly Dictionary<States, BaseState> states = new Dictionary<States, BaseState>();
        private BaseState currentState;

        private void Start()
        {
            var statesArray = GetComponentsInChildren<BaseState>(true);
           
            for (int i = 0; i < statesArray.Length; i++)
            {
                var state = statesArray[i];
                states.Add(state.GetStateType, state);
            }

            InitStates(States.MainMenu);
        }

        public void ChangeState(States state)
        {
            currentState.Disable();

            currentState = states[state];

            currentState.Enable();
        }

        private void InitStates(States startState)
        {
            foreach (var item in states)
            {
                if (item.Key == startState)
                {
                    currentState = item.Value;

                    currentState.Enable();
                }
                else
                {
                    item.Value.Disable();
                }
            }
        }
    }
}