
using System.Collections.Generic;

namespace Dotity
{
    public abstract class RenderSystem_V2 : BaseSystem_V2, IRenderSystem
    {
        public RenderSystem_V2(IMatcher matcher) : base(matcher) { }
        public abstract void Render();
    }
}

