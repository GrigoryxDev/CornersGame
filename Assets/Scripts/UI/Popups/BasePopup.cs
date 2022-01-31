using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Zenject;
using Assets.Scripts.PopupSpawnSystem;

namespace Assets.Scripts.UI.Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] protected Button closeButton;

        private PopupFactory popupSpawner;
        private float scaleAnimationTime;
        public event Action EventOnClose;


        [Inject]
        private void Constructor(PopupSettingsSO popupSettingsSO, PopupFactory popupSpawner)
        {
            scaleAnimationTime = popupSettingsSO.GetScaleAnimationTime;
            this.popupSpawner = popupSpawner;
        }

        protected virtual void Awake()
        {
            closeButton.onClick.AddListener(CLose);
        }

        protected virtual void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, scaleAnimationTime);
        }

        protected virtual void CLose()
        {
            EventOnClose?.Invoke();
            popupSpawner.ClosePopup(this);
        }

    }
}