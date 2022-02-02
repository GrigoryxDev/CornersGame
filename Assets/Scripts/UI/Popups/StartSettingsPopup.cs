using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Popups
{
    public class StartSettingsPopup : BasePopup
    {
        [SerializeField] private Toggle diagonallyToggle;
        [SerializeField] private Toggle upAndDownToggle;

        public (bool diagonally, bool upAndDown) GetTogglesInfo()
        {
            return (diagonallyToggle.isOn, upAndDownToggle.isOn);
        }
    }
}