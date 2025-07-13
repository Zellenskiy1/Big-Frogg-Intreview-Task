using UnityEngine;

namespace Grab
{
    internal class GrabView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rope;
        [SerializeField] private Transform claw;

        internal SpriteRenderer Rope => rope;
        internal Transform Claw => claw;
    }
}