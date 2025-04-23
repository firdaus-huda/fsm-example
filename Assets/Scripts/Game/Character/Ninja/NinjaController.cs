using System;
using NinjaFSM.Common;
using NinjaFSM.Game.Common;
using UnityEngine;

namespace NinjaFSM.Game.Character
{
    public class NinjaController : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterData data;
        
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private CapsuleCollider2D characterCollider;
        [SerializeField] private Transform attackCheck;

        public float MoveInput { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsInvincible { set => _model.isInvincible = value; }
        public Rigidbody2D RigidBody => rigidBody;
        
        private readonly StateMachine _stateMachine = new();
        private readonly CharacterModel _model = new();

        public event Action<int> CurrentHealthChanged;

        private void Awake()
        {
            _model.currentHealth = data.maxHealth;
            _model.isInvincible = false;
            characterCollider.size = data.initialColliderSize;
            
            CurrentHealthChanged?.Invoke(_model.currentHealth);
        }

        private void Start()
        {
            ChangeState(new NinjaIdleState(this));
        }

        private void Update()
        {
            CheckInput();
            CheckGrounded();

            _stateMachine.Update();
        }

        private void FixedUpdate() => _stateMachine.FixedUpdate();
        public void ChangeState(IState state) => _stateMachine.ChangeState(state);
        public void SetAnimation(string animationName) => animator.Play(animationName);

        public void SetAnimationParameter(string parameter, float value) => animator.SetFloat(parameter, value);

        public void HandleMovement()
        {
            rigidBody.linearVelocityX = MoveInput * data.moveSpeed;

            Flip(MoveInput);
        }

        public void HandleGravity()
        {
            rigidBody.gravityScale = rigidBody.linearVelocity.y < 0 ? data.gravityScale * data.fallMultiplier : data.gravityScale;
        }

        public void Jump()
        {
            if (!IsGrounded) return;
            rigidBody.AddForce(new Vector2(0f, data.jumpForce), ForceMode2D.Impulse);
        }

        public void Attack()
        {
            var hit = Physics2D.OverlapBox(attackCheck.position, data.attackCheckArea, data.attackLayer);
            if (hit != null && hit.GetComponent<IDamageable>() is {} damageable) damageable.Damage(transform.position);
        }
        
        public void ApplyKnockBack(Vector2 knockBack)
        {
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.AddForce(knockBack, ForceMode2D.Impulse);
        }

        public void Damage(Vector2 attackPosition)
        {
            if (_model.isInvincible || _stateMachine.CurrentState is NinjaDieState) return;
            
            _model.currentHealth--;
            CurrentHealthChanged?.Invoke(_model.currentHealth);
            
            Vector2 knockDir = (transform.position - (Vector3)attackPosition).normalized;
            Vector2 knockBack = knockDir * data.knockBackStrength + Vector2.up * 5f;
            
            _model.isInvincible = true;

            if (_model.currentHealth <= 0)
            {
                characterCollider.size = data.deadColliderSize;
                ApplyKnockBack(knockBack);
                ChangeState(new NinjaDieState(this));
            }
            else ChangeState(new NinjaHurtState(this, knockBack, data.invincibilityDuration));
        }

        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.groundCheckLength, data.groundLayer);
            IsGrounded = hit.collider != null;

            if (IsGrounded) rigidBody.linearDamping = data.groundDamping;
        }

        private void Flip(float direction)
        {
            if (direction != 0) transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
        
        private void CheckInput()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            JumpPressed = Input.GetButtonDown("Jump");
            AttackPressed = Input.GetButtonDown("Fire1");
        }
    }
}
