using UnityEngine;

namespace NinjaFSM.Game.Character
{
    public class NinjaHurtState : NinjaState
    {
        private float _timer;
        private readonly Vector2 _knockBack;
        private readonly float _duration;

        public NinjaHurtState(NinjaController ninja, Vector2 knockBack, float duration) : base(ninja)
        {
            _knockBack = knockBack;
            _duration = duration;
        }

        public override void Enter()
        {
            _timer = 0f;
            Controller.ApplyKnockBack(_knockBack);
            Controller.SetAnimation("Idle");
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _duration)
            {
                if (Controller.IsGrounded)
                    Controller.ChangeState(new NinjaIdleState(Controller));
                else
                    Controller.ChangeState(new NinjaJumpState(Controller));
            }
        }

        public override void Exit()
        {
            Controller.IsInvincible = false;
        }
    }
}