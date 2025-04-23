using UnityEngine;

namespace NinjaFSM.Game.Character
{
    public class NinjaAttackState : NinjaState
    {
        private float _attackDuration = 0.3f;
        private float timer = 0f;

        public NinjaAttackState(NinjaController controller) : base(controller) { }

        public override void Enter()
        {
            Controller.SetAnimation(Controller.IsGrounded ? "Attack" : "JumpAttack");
            timer = 0f;
            Controller.Attack();
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= _attackDuration)
            {
                if (Controller.IsGrounded)
                    Controller.ChangeState(new NinjaIdleState(Controller));
                else
                    Controller.ChangeState(new NinjaJumpState(Controller));
            }
        }
    }
}