using Cargo.Enums;
using Cargo.Interfaces;
using Cargo.View;
using Core.Systems;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Cargo.Controllers
{
    internal class CargoFactory
    {
        private readonly PrefabsContainer _data;
        private readonly EventBus _eventBus;
        private int _cargoAmount = 0;

        internal CargoFactory(PrefabsContainer data, EventBus eventBus)
        {
            _eventBus = eventBus;
            _data = data;
        }

        internal void Launch(int limit = 25)
        {
            var parent = new GameObject
            {
                name = "Container"
            };
            SpawnLoop(parent.transform, limit);
        }

        private void SpawnLoop(Transform parent, int limit)
        {
            if (_cargoAmount >= limit)
            {
                return;
            }
            _cargoAmount++;

            var isBlue = Random.value > 0.5f;
            var cargoType = isBlue ? CargoType.Blue : CargoType.Red;
            var cargoView = isBlue ? _data.GetBlueCargo : _data.GetRedCargo;

            var instance = Object.Instantiate(cargoView, parent);
            var cargoController = new CargoController(instance, cargoType);

            var newCargo = (ICargo)cargoController;
            newCargo.Fall(2f, -4.42f).OnComplete(() => { _eventBus.OnCargoAvailable(newCargo); });

            DOVirtual.DelayedCall(Random.Range(5f, 7f), () => SpawnLoop(parent.transform, limit));
        }
    }
}