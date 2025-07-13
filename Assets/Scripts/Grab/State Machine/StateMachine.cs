using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grab.State_Machine
{
    internal class StateMachine<T> where T : IState
    {
        internal bool IsAvailable { get; private set; } = true;
        internal event Action PathCompleted;
        private readonly IGrab _grab;
        private T _currentState;
        private List<T> _moves;

        internal StateMachine(IGrab grab)
        {
            _grab = grab;
        }

        private IState SetNext()
        {
            if (_moves.Count <= 0)
            {
                PathCompleted?.Invoke();
                IsAvailable = true;
                return null;
            }

            _currentState = _moves.First();
            _moves.RemoveAt(0);
            return _currentState;
        }

        internal void BuildPath(List<T> moves)
        {
            IsAvailable = false;
            _moves = moves;
            _currentState = _moves.First();
            _currentState.Do(SetNext);
        }
    }
}