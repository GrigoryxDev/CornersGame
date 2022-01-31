using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public abstract class BaseFactory : MonoBehaviour
    {
        protected DiContainer diContainer;

        [Inject]
        private void Constructor(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Spawn<T>(string resourcesPath, Transform parent)
        {
            var obj = diContainer.InstantiatePrefabResourceForComponent<T>(resourcesPath, parent);
            return obj;
        }

        public T Spawn<T>(string resourcesPath, Transform parent, Vector3 position, Quaternion rotation)
        {
            var obj = diContainer.InstantiatePrefabResourceForComponent<T>(resourcesPath, position, rotation, parent);
            return obj;
        }
    }
}