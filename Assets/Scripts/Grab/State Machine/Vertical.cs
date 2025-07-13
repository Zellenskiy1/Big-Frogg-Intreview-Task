using System;

namespace Grab.State_Machine
{
    internal class Vertical : IState
    {
        private readonly Direction _direction;
        private readonly IGrab _grab;

        public Vertical(IGrab grab, Direction direction)
        {
            _direction = direction;
            _grab = grab;
        }

        void IState.Do(Func<IState> onComplete)
        {
            _grab.MoveVertical(_direction, () => { onComplete?.Invoke().Do(onComplete); });
        }

        internal enum Direction
        {
            Down,
            Up,
        }
    }
}