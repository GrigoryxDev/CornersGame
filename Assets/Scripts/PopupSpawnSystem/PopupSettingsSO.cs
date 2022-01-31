using Zenject;
using UnityEngine;

namespace Assets.Scripts.PopupSpawnSystem
{


    [CreateAssetMenu(fileName = "PopupSettingsSO", menuName = "ScriptableObject/PopupSettingsSO", order = 0)]
    public class PopupSettingsSO : ScriptableObjectInstaller<PopupSettingsSO>
    {
        [SerializeField] private float poppupScaleAnimationTime = .25f;
        [SerializeField] private float matePanelAlpha = 220;
        [SerializeField] private float matePanelFadeTime = .3f;

        public float GetScaleAnimationTime => poppupScaleAnimationTime;
        public float GetMatePanelAlpha => matePanelAlpha;
        public float GetMatePanelFadeTime => matePanelFadeTime;


        public override void InstallBindings()
        {
            Container.Bind<PopupSettingsSO>()
            .FromInstance(this)
            .AsSingle().NonLazy();
        }
    }
}