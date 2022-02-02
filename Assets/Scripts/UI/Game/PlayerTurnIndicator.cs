using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Game
{
    public class PlayerTurnIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI indicatorText;

        private Sequence sequence;

        public void Show(string name)
        {
            indicatorText.text = $"{name} turn";
            sequence?.Kill();
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            indicatorText.color = Color.white;
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(1.2f, .6f))
            .Append(transform.DOScale(.1f, .7f))
            .Join(indicatorText.DOColor(Color.clear, .7f))
            .SetEase(Ease.InOutElastic)
            .OnComplete(() => Hide());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}