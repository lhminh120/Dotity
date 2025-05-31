
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Dotity
{
    public class SystemManager
    {
        private readonly List<IServiceSystem> _serviceSystems = new List<IServiceSystem>();
        private readonly List<IInitializeSystem> _initializeSystems = new List<IInitializeSystem>();
        private readonly List<IExecuteSystem> _executeSystems = new List<IExecuteSystem>();
        private readonly List<IRenderSystem> _renderSystems = new List<IRenderSystem>();
        private readonly List<ICleanUpSystem> _cleanUpSystems = new List<ICleanUpSystem>();
        public SystemManager Add(ISystem system)
        {
            IServiceSystem serviceSystem = system as IServiceSystem;
            if (serviceSystem != null)
            {
                _serviceSystems.Add(serviceSystem);
            }
            IInitializeSystem initializeSystem = system as IInitializeSystem;
            if (initializeSystem != null)
            {
                _initializeSystems.Add(initializeSystem);
            }
            IExecuteSystem executeSystem = system as IExecuteSystem;
            if (executeSystem != null)
            {
                _executeSystems.Add(executeSystem);
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
        public void ServiceExecute()
        {
            for (int i = 0, length = _serviceSystems.Count; i < length; i++)
            {
                _serviceSystems[i].ServiceExecute();
            }
        }
        public void Initialize()
        {
            for (int i = 0, length = _initializeSystems.Count; i < length; i++)
            {
                _initializeSystems[i].Initialize();
            }
        }
        public void Execute()
        {
            for (int i = 0, length = _executeSystems.Count; i < length; i++)
            {
                _executeSystems[i].Execute();
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
