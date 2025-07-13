using Cargo.Controllers;
using Cargo.Enums;
using Core.Systems;
using Data;
using Grab;
using UnityEditor;
using UnityEngine;

namespace Core
{
    internal static class Main
    {
        [InitializeOnLoadMethod]
        private static void InitializeProject() => EditorApplication.delayCall += Configure;

        private static void Configure()
        {
            if (!EditorApplication.isPlaying) return;

            var data = Resources.Load<PrefabsContainer>("Container");
            var eventBus = new EventBus();
            new CargoFactory(data, eventBus).Launch();

            var crane = GameObject.FindGameObjectWithTag("Crane");
            var grabView = Object.Instantiate(data.GetGrab, crane.transform);

            var blueStorageView = new GameObject
            {
                name = "Blue Cargo Storage"
            };
            
            var redStorageView = new GameObject
            {
                name = "Red Cargo Storage"
            };
            
            var blueStorage = new CargoStorage(blueStorageView.transform, CargoType.Blue);
            var redStorage = new CargoStorage(redStorageView.transform, CargoType.Red);
            var grabController = new GrabController(grabView, eventBus, blueStorage, redStorage);
        }
    }
}