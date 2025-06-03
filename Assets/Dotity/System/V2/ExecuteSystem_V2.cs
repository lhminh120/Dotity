using System.Collections.Generic;

namespace Dotity
{
    public abstract class ExecuteSystem_V2 : BaseSystem_V2, IExecuteSystem
    {
        public ExecuteSystem_V2(IMatcher matcher) : base(matcher) { }
        public abstract void Execute();
    }
}

