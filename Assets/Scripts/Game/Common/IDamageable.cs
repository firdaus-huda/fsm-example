using UnityEngine;

namespace NinjaFSM.Game.Common
{
    public interface IDamageable
    {
        void Damage(Vector2 attackPosition);
    }
}