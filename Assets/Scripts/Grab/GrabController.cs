using System;
using System.Collections.Generic;
using System.Linq;
using Cargo.Controllers;
using Cargo.Enums;
using Cargo.Interfaces;
using Core.Extensions;
using Core.Systems;
using DG.Tweening;
using Grab.State_Machine;
using UnityEngine;

namespace Grab
{
    internal class GrabController : IGrab
    {
        private const float ChangeStateTime = 0.2f;
        private readonly StateMachine<IState> _stateMachine;
        private readonly List<ICargo> _cargoes = new();
        private readonly GrabView _view;
        private readonly CargoStorage _blue;
        private readonly CargoStorage _red;

        internal GrabController(GrabView view, EventBus eventBus,
            CargoStorage blueStorage, CargoStorage redStorage)
        {
            _stateMachine = new StateMachine<IState>(this);
            _stateMachine.PathCompleted += OnPathCompleted;

            _blue = blueStorage;
            _red = redStorage;

            _view = view;
            _view.transform.localPosition = Vector3.zero;

            eventBus.SubscribeToCargoReady(OnCargoIsReadyToPickUp);
            GrabLoopWithDoTween();
        }

        #region IMovable

        public void MoveVertical(Vertical.Direction direction, Action callback)
        {
            const float duration = 2f;
            const float distance = 0.8f;

            var clawOffset = direction == Vertical.Direction.Down ? distance : -distance;
            var ropeOffset = direction == Vertical.Direction.Down ? -distance : distance;

            _view.Claw.DOLocalMoveY(_view.Claw.transform.localPosition.y + clawOffset, duration)
                .SetEase(Ease.OutSine);
            _view.Rope.DOSizeY(_view.Rope.size.y + ropeOffset, duration).SetEase(Ease.OutSine)
                .OnComplete(() => callback?.Invoke());
        }

        public void MoveHorizontal(float x, Action callback)
        {
            _view.transform.DOMoveX(x, 1f).SetDelay(ChangeStateTime).SetEase(Ease.Linear)
                .OnComplete(() => callback?.Invoke());
        }

        public void Catch(ICargo cargo, Action callback)
        {
            DOVirtual.DelayedCall(ChangeStateTime, () =>
            {
                cargo.SetParent(_view.Claw);
                callback?.Invoke();
            });
        }

        public void Release(ICargo cargo, Action callback)
        {
            DOVirtual.DelayedCall(ChangeStateTime, () =>
            {
                var storage = cargo.GetCargoType == CargoType.Blue ? _blue : _red;
                cargo.SetParent(null);
                cargo.Fall(1f, storage.GetAvailablePosition()).OnComplete(() => cargo.SetParent(storage.View));
                callback?.Invoke();
            });
        }

        #endregion

        private void GrabLoopWithDoTween()
        {
            DOTween.Sequence().AppendCallback(() =>
            {
                if (!_stateMachine.IsAvailable || _cargoes.Count <= 0) return;

                _stateMachine.BuildPath(new List<IState>()
                {
                    new Horizontal(this, _cargoes.First().GetPositionX()),
                    new Vertical(this, Vertical.Direction.Down),
                    new Catch(this, _cargoes.First()),
                    new Vertical(this, Vertical.Direction.Up),
                    new Horizontal(this, _cargoes.First().GetCargoType == CargoType.Blue ? 5.8f : -5.8f),
                    new Release(this, _cargoes.First())
                });
            }).AppendInterval(2f).SetLoops(-1);
        }

        private void OnCargoIsReadyToPickUp(ICargo obj) => _cargoes.Add(obj);
        private void OnPathCompleted() => _cargoes.RemoveAt(0);
    }
}