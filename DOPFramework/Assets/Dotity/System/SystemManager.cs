
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Dotity
{
    public class SystemManager
    {
        private List<IInitializeSystem> _initializeSystems = new List<IInitializeSystem>();
        private List<IExcuteSystem> _excuteSystems = new List<IExcuteSystem>();
        private List<ICleanUpSystem> _cleanUpSystem = new List<ICleanUpSystem>();
        public void Add(ISystem system)
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
            ICleanUpSystem cleanUpSystem = system as ICleanUpSystem;
            if (cleanUpSystem != null)
            {
                _cleanUpSystem.Add(cleanUpSystem);
            }
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
        public void CleanUp()
        {
            for (int i = 0, length = _cleanUpSystem.Count; i < length; i++)
            {
                _cleanUpSystem[i].CleanUp();
            }
        }
    }

}
