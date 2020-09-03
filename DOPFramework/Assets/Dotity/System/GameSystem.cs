
using UnityEngine;
namespace Dotity
{
    public abstract class GameSystem : MonoBehaviour
    {
        protected SystemManager _systemManager = new SystemManager();
        public static float _tick = 0.01667f;
        private void Awake()
        {
            InitSystem();
            _systemManager.Initialize();
        }
        private void Update()
        {
            _tick = Time.deltaTime;
            _systemManager.ServiceExcute();
            _systemManager.Excute();
            _systemManager.Render();
            _systemManager.CleanUp();
        }
        protected abstract void InitSystem();
    }
}

