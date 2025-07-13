using Cargo.Enums;
using Core.Systems;
using DG.Tweening;
using UnityEngine;

namespace Cargo.Interfaces
{
    internal interface ICargo
    {
        internal CargoType GetCargoType { get; }
        
        internal Tween Fall(float duration, float endPosition);

        internal void SetParent(Transform transform);

        internal float GetPositionX();

    }
}