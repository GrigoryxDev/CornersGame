using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Popups
{
    public class StartSettingsPopup : BasePopup
    {
        [SerializeField] private Toggle diagonallyToggle;
        [SerializeField] private Toggle vertAndHorizToggle;

        public (bool diagonally, bool vertAndHorizToggle) GetTogglesInfo()
        {
            return (diagonallyToggle.isOn, vertAndHorizToggle.isOn);
        }
    }
}