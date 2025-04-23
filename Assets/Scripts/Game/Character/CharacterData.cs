using System;
using UnityEngine;

namespace NinjaFSM.Game.Character
{
    [Serializable]
    public class CharacterData
    {
        [Header("Movement")]
        public float moveSpeed;
        public float jumpForce;
        public float gravityScale;
        public float fallMultiplier;
        public float groundDamping;

        [Header("Collision Check")] 
        public float groundCheckLength;
        public LayerMask groundLayer;
        public Vector2 attackCheckArea;
        public LayerMask attackLayer;

        [Header("Combat")] 
        public int maxHealth;
        public float invincibilityDuration;
        public float knockBackStrength;

        [Header("Other")] 
        public Vector2 initialColliderSize;
        public Vector2 deadColliderSize;
    }
}