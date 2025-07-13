using System;
using System.Collections.Generic;
using Cargo.Interfaces;

namespace Core.Systems
{
    internal class EventBus
    {
        internal event Action<ICargo> CargoIsReadyToPickUp;
        internal void SubscribeToCargoReady(Action<ICargo> listener) => CargoIsReadyToPickUp += listener;
        internal void OnCargoAvailable(ICargo cargo)
        {
            CargoIsReadyToPickUp?.Invoke(cargo);
        }
    }
}