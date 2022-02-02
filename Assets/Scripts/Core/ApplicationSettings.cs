using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ApplicationSettings : MonoBehaviour
    {
        [SerializeField] private int fpsLimit = 60;
        private void Awake()
        {
            Application.targetFrameRate = fpsLimit;
        }
    }
}