using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.PopupSpawnSystem
{
    public class MatePanel : MonoBehaviour
    {
        private float targetAlpha;
        private float fadeTime;
        private Sequence seq;

        private Image mateImage;
        private Image GetMateImage => mateImage ?? (mateImage = GetComponent<Image>());

        public bool IsShowed => gameObject.activeSelf;


        [Inject]
        private void Constructor(PopupSettingsSO popupSettingsSO)
        {
            targetAlpha = popupSettingsSO.GetMatePanelAlpha;
            fadeTime = popupSettingsSO.GetMatePanelFadeTime;
        }

        public void Show()
        {
            if (IsShowed)
            {
                return;
            }

            GetMateImage.DOFade(0, 0);
            seq.Kill();
            seq = DOTween.Sequence();
            seq.AppendCallback(() => SetActive(true))
            .Append(GetMateImage.DOFade(GetFromRGBFloatColorValue(targetAlpha), fadeTime));
        }

        public void Hide()
        {
            if (!IsShowed)
            {
                return;
            }
            
            GetMateImage.DOFade(1, 0);
            seq.Kill();
            seq = DOTween.Sequence();
            seq.Append(GetMateImage.DOFade(0, fadeTime))
            .OnComplete(() =>
            {
                SetActive(false);
            });
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private float GetFromRGBFloatColorValue(float value)
        {
            if (value == 0)
            {
                return 0;
            }
            return value / 255;
        }
    }
}