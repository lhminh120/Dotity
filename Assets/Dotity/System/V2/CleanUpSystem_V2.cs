using System.Collections.Generic;

namespace Dotity
{
    public abstract class CleanUpSystem_V2 : BaseSystem_V2, ICleanUpSystem
    {
        public CleanUpSystem_V2(IMatcher matcher) : base(matcher) { }
        public abstract void CleanUp();
    }
}

