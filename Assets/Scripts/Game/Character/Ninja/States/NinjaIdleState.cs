using UnityEngine;

namespace NinjaFSM.Game.Character
{
    public class NinjaIdleState : NinjaState
    {
        public NinjaIdleState(NinjaController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.SetAnimation("Idle");
        }

        public override void Update()
        {
            if (Mathf.Abs(Controller.MoveInput) > 0.1f)
                Controller.ChangeState(new NinjaRunState(Controller));
            else if (Controller.JumpPressed && Controller.IsGrounded)
                Controller.ChangeState(new NinjaJumpState(Controller));
            else if (Controller.AttackPressed)
                Controller.ChangeState(new NinjaAttackState(Controller));
        }
    }
}