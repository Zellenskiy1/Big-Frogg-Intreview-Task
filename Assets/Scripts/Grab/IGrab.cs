using System;
using Cargo.Interfaces;
using Grab.State_Machine;

namespace Grab
{
    internal interface IGrab
    {
        public void MoveVertical(Vertical.Direction direction, Action callback);
        public void MoveHorizontal(float x, Action callback);
        public void Release(ICargo cargo, Action callback);
        public void Catch(ICargo cargo, Action callback);
    }
}