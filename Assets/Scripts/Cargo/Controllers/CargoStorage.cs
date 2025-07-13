using Cargo.Enums;
using DG.Tweening;
using UnityEngine;

namespace Cargo.Controllers
{
    internal class CargoStorage
    {
        private readonly float _offset = 1.175f;
        private float _lastPosition = -5.69f;
        private int _cargoesAmount = 0;

        public Transform View => _view;
        private Transform _view;
        private CargoType _type;

        internal CargoStorage(Transform view, CargoType type)
        {
            _view = view;
            _type = type;
        }

        internal float GetAvailablePosition()
        {
            _cargoesAmount++;
            if (_cargoesAmount >= Random.Range(3, 5))
                _view.DOLocalMoveY(_view.localPosition.y - _offset, 0.8f).SetEase(Ease.Linear);
            else _lastPosition += _offset;
            return _lastPosition;
        }
    }
}