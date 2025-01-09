using UnityEngine;

namespace Assets.Scripts.GamePlay.Movement
{
    public interface IMove
    {
        void Move(Vector3 direction);
        void Jump(Vector3 direction);
        void Dash(Vector3 direction);
        void Sit();
    }
}
