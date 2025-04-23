using System;
using NinjaFSM.Game.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NinjaFSM.Game.Character
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D collider2d;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float moveSpeed;

        private Vector2 _direction;
        public event Action<EnemyController> Killed;

        private void Start()
        {
            _direction = Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
            Flip();
        }

        private void Update()
        {
            Move();
            CheckWall();
        }
        
        void CheckWall()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, wallCheckDistance, collisionLayer);
            if (hit.collider != null)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _direction *= -1f;
            transform.localScale = new Vector3(Mathf.Sign(_direction.x), 1, 1);
        }
        
        private void Move()
        {
            rigidBody.MovePosition((Vector2)transform.position + _direction * moveSpeed);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.GetComponent<EnemyController>() is not null)
            {
                Physics2D.IgnoreCollision(collider2d, other.collider, true);
                return;
            }
            
            if (other.collider.GetComponent<IDamageable>() is { } damageable and not EnemyController)
            {
                damageable.Damage(transform.position);
            }
        }
        
        public void Damage(Vector2 attackPosition)
        {
            Killed?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Killed = null;
        }
    }
}