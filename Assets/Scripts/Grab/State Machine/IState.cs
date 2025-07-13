using System;

namespace Grab.State_Machine
{
    internal interface IState
    {
        internal void Do(Func<IState> onComplete);
    }
}