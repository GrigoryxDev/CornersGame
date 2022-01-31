using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.StateView
{
    public class MainMenuStateView : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private CanvasGroup stateGroup;

        public CanvasGroup GeStateCanvasGroup => stateGroup;
        public Button GeStartGameButton => startGameButton;

    }
}