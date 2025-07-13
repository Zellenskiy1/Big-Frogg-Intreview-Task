using Cargo.Interfaces;
using Cargo.View;
using Grab;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Prefabs container")]
    internal class PrefabsContainer : ScriptableObject
    {
        [SerializeField] private CargoView blueCargo;
        [SerializeField] private CargoView redCargo;
        [SerializeField] private GrabView grab;

        internal CargoView GetBlueCargo => blueCargo;
        internal CargoView GetRedCargo => redCargo;
        internal GrabView GetGrab => grab;
    }
}