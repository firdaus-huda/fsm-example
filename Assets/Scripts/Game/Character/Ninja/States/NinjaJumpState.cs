namespace NinjaFSM.Game.Character
{
    public class NinjaJumpState : NinjaState
    {
        public NinjaJumpState(NinjaController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.SetAnimation("Jump");
            Controller.Jump();
        }

        public override void Update()
        {
            Controller.SetAnimationParameter("verticalVelocity", Controller.RigidBody.linearVelocityY);
            if (Controller.RigidBody.linearVelocity.y <= 0 && Controller.IsGrounded)
                Controller.ChangeState(new NinjaIdleState(Controller));
            else if (Controller.AttackPressed)
                Controller.ChangeState(new NinjaAttackState(Controller));
        }
        
        public override void FixedUpdate()
        {
            Controller.HandleMovement();
            Controller.HandleGravity();
        }
    }
}