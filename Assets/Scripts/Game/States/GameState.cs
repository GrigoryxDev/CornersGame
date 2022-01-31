using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.UI.StateView;

namespace Assets.Scripts.Game.States
{
    public class GameState : BaseState
    {
        [SerializeField] private GameStateView gameStateView;

        public override States GetStateType => States.Game;

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