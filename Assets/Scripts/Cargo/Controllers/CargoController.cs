using Cargo.Enums;
using Cargo.Interfaces;
using Cargo.View;
using DG.Tweening;
using UnityEngine;

namespace Cargo.Controllers
{
    internal class CargoController : ICargo
    {
        private readonly CargoView _cargoView;
        private readonly CargoType _cargoType;

        public CargoController(CargoView cargoView, CargoType cargoType)
        {
            _cargoView = cargoView;
            _cargoType = cargoType;
            _cargoView.transform.position = new Vector3(Random.Range(-3.3f, 3.3f), 5f, 0f);
        }

        #region ICargo

        CargoType ICargo.GetCargoType => _cargoType;

        Tween ICargo.Fall(float duration, float endPos)
        {
            return _cargoView.transform.DOMoveY(endPos, duration).SetEase(Ease.InCubic);
        }

        void ICargo.SetParent(Transform transform)
        {
            _cargoView.transform.SetParent(transform);
        }

        float ICargo.GetPositionX() => _cargoView.transform.position.x;

        #endregion
    }
}