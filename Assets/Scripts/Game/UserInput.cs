using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;
using Assets.Scripts.Game.GameBoard;
using Zenject;

namespace Assets.Scripts.Game
{
    public class UserInput
    {
        private GameElementsMover gameElementsMover;
        private GameObserver gameObserver;


        public event Action EventOnElementMoved;

        [Inject]
        private void Constructor(GameElementsMover gameElementsMover, GameObserver gameObserver)
        {
            this.gameElementsMover = gameElementsMover;
            this.gameObserver = gameObserver;
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
            .Where(x => IsInputActive())
            .Subscribe(x =>
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);

                for (int index = 0; index < raysastResults.Count; index++)
                {
                    RaycastResult rayCsastResult = raysastResults[index];
                    if (rayCsastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        if (rayCsastResult.gameObject.TryGetComponent<ChipHolder>(out var checkedChipHolder))
                        {
                            if (gameElementsMover.TryMove(checkedChipHolder) == MoverResult.Move)
                            {
                                EventOnElementMoved?.Invoke();
                            }

                            return;
                        }
                    }
                }
            });
        }

        private bool IsInputActive()
        {
            bool active = Input.GetMouseButtonDown(0) && gameObserver.CurrentGameStatus == GameStatus.Active;
            return active;
        }
    }
}