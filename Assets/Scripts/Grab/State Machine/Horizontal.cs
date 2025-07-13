using System;

namespace Grab.State_Machine
{
    internal class Horizontal : IState
    {
        private readonly float _positionX;
        private readonly IGrab _grab;

        public Horizontal(IGrab grab, float x)
        {
            _grab = grab;
            _positionX = x;
        }

        void IState.Do(Func<IState> onComplete)
        {
            _grab.MoveHorizontal(_positionX, () => { onComplete?.Invoke().Do(onComplete); });
        }
    }
}