using System;
using Cargo.Interfaces;

namespace Grab.State_Machine
{
    internal class Catch : IState
    {
        private readonly IGrab _grab;
        private readonly ICargo _cargo;

        public Catch(IGrab grab, ICargo cargo)
        {
            _grab = grab;
            _cargo = cargo;
        }

        void IState.Do(Func<IState> onComplete) => _grab.Catch(_cargo, () => { onComplete?.Invoke().Do(onComplete); });
    }
}