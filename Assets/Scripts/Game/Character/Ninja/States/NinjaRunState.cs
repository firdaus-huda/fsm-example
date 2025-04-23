using UnityEngine;

namespace NinjaFSM.Game.Character
{
    public class NinjaRunState: NinjaState
    {
        public NinjaRunState(NinjaController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.SetAnimation("Run");
        }

        public override void Update()
        {
            if (Mathf.Abs(Controller.MoveInput) < 0.1f)
                Controller.ChangeState(new NinjaIdleState(Controller));
            else if (Controller.JumpPressed && Controller.IsGrounded)
                Controller.ChangeState(new NinjaJumpState(Controller));
            else if (Controller.AttackPressed)
                Controller.ChangeState(new NinjaAttackState(Controller));
        }

        public override void FixedUpdate()
        {
            Controller.HandleMovement();
        }

        public override void Exit()
        {
            if (Controller.IsGrounded) Controller.RigidBody.linearVelocityX = 0f;
        }
    }
}