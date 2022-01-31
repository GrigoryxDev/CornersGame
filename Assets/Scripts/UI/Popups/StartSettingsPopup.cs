using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Popups
{
    public class StartSettingsPopup : BasePopup
    {
        [SerializeField] private Toggle diagonalyToggle;
        [SerializeField] private Toggle upAndDownToggle;

        public (bool diagonaly, bool upAndDown) GetTogglesInfo()
        {
            return (diagonalyToggle.isOn, upAndDownToggle.isOn);
        }
    }
}