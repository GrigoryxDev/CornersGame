using UnityEngine;

namespace Assets.Scripts.Game.GameBoard
{
    public interface IPlayerElement
    {
        Transform GetTransform { get; }
        void Select();

        void Deselect();

        void ChangePosition(Vector2 newPosition);
    }
}