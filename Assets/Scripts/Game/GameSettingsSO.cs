using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "ScriptableObject/GameSettingsSO", order = 0)]
    public class GameSettingsSO : ScriptableObjectInstaller<GameSettingsSO>
    {
        [SerializeField] private float moveAnimationTime = .25f;
        [SerializeField] private float fadeTime = .4f;

        public float GetMoveAnimationTime => moveAnimationTime;
        public float GetFadeTime => fadeTime;

        public override void InstallBindings()
        {
            Container.Bind<GameSettingsSO>()
            .FromInstance(this)
            .AsSingle().NonLazy();
        }
    }
}