using System;
using System.Collections.Generic;
using Zenject;
using System.IO;
using UniRx;
using Assets.Scripts.Core;
using Assets.Scripts.UI.Popups;

namespace Assets.Scripts.PopupSpawnSystem
{
    public class PopupFactory : BaseFactory
    {
        private const string resourcesPath = "Popups/";
        private readonly List<BasePopup> spawned = new List<BasePopup>();

        [Inject]
        private void Constructor(MatePanel matePanel)
        {
            spawned.ObserveEveryValueChanged(x => x.Count).Subscribe(count =>
            {
                if (count > 0)
                {
                    matePanel.Show();
                }
                else
                {
                    matePanel.Hide();
                }
            }).AddTo(this);

            matePanel.SetActive(false);
        }

        public void SpawnPopup<T>(string popupName, Action<T> onSpawn = null) where T : BasePopup
        {
            var path = Path.Combine(resourcesPath, popupName);

            var popup = Spawn<T>(path, transform);
            spawned.Add(popup);
            onSpawn?.Invoke(popup);
        }

        public void ClosePopup(BasePopup basePopup)
        {
            spawned.Remove(basePopup);
            Destroy(basePopup.gameObject);
        }

    }
}