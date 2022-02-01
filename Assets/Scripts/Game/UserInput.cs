using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;
using Assets.Scripts.Game.GameBoard;

namespace Assets.Scripts.Game
{
    public class UserInput : MonoBehaviour
    {
        private IDisposable inputObservable;
        private PlayerChip selectedPlayerChip;

        public void Active()
        {
            inputObservable?.Dispose();
            inputObservable = Observable.EveryUpdate().Where(x => Input.GetMouseButtonDown(0))
            .Subscribe(x =>
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> raysastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raysastResults);

                for (int index = 0; index < raysastResults.Count; index++)
                {
                    RaycastResult curRaysastResult = raysastResults[index];
                    if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        if (curRaysastResult.gameObject.TryGetComponent<PlayerChip>(out var playerChip))
                        {
                            if (selectedPlayerChip != null)
                            {
                                //TODO: check 

                            }

                        }
                        else if (curRaysastResult.gameObject.TryGetComponent<ChipHolder>(out var chipHolder))
                        {

                        }
                    }
                }
            }).AddTo(this);
        }

        public void Disable()
        {
            inputObservable?.Dispose();
        }
    }
}