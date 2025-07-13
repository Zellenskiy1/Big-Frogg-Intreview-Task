using System;
using Cargo.Interfaces;

namespace Grab.State_Machine
{
    internal class Release : IState
    {
        private readonly IGrab _grab;
        private readonly ICargo _cargo;

        public Release(IGrab grab, ICargo cargo)
        {
            _grab = grab;
            _cargo = cargo;
        }

        void IState.Do(Func<IState> onComplete) =>
            _grab.Release(_cargo, () =>
            {
                onComplete?.Invoke()?.Do(onComplete);
            });
    }
}