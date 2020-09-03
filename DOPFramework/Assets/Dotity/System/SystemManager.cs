
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Dotity
{
    public class SystemManager
    {
        private readonly List<IInitializeSystem> _initializeSystems = new List<IInitializeSystem>();
        private readonly List<IExcuteSystem> _excuteSystems = new List<IExcuteSystem>();
        private readonly List<IRenderSystem> _renderSystems = new List<IRenderSystem>();
        private readonly List<ICleanUpSystem> _cleanUpSystems = new List<ICleanUpSystem>();
        public SystemManager Add(ISystem system)
        {
            IInitializeSystem initializeSystem = system as IInitializeSystem;
            if (initializeSystem != null)
            {
                _initializeSystems.Add(initializeSystem);
            }
            IExcuteSystem excuteSystem = system as IExcuteSystem;
            if (excuteSystem != null)
            {
                _excuteSystems.Add(excuteSystem);
            }
            IRenderSystem renderSystem = system as IRenderSystem;
            if (renderSystem != null)
            {
                _renderSystems.Add(renderSystem);
            }
            ICleanUpSystem cleanUpSystem = system as ICleanUpSystem;
            if (cleanUpSystem != null)
            {
                _cleanUpSystems.Add(cleanUpSystem);
            }
            return this;
        }

        public void Initialize()
        {
            for (int i = 0, length = _initializeSystems.Count; i < length; i++)
            {
                _initializeSystems[i].Initialize();
            }
        }
        public void Excute()
        {
            for (int i = 0, length = _excuteSystems.Count; i < length; i++)
            {
                _excuteSystems[i].Excute();
            }
        }
        public void Render()
        {
            for (int i = 0, length = _renderSystems.Count; i < length; i++)
            {
                _renderSystems[i].Render();
            }
        }
        public void CleanUp()
        {
            for (int i = 0, length = _cleanUpSystems.Count; i < length; i++)
            {
                _cleanUpSystems[i].CleanUp();
            }
        }
    }

}
