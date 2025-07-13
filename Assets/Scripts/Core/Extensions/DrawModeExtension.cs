using DG.Tweening;
using UnityEngine;

namespace Core.Extensions
{
    internal static class DrawModeExtension
    {
        internal static Tweener DOSizeY(this SpriteRenderer renderer, float target, float duration)
        {
            return DOTween.To(
                () => renderer.size.y,
                y => renderer.size = new Vector2(renderer.size.x, y),
                target, duration);
        }
    }
}