namespace NinjaFSM.Game.Character
{
    public class NinjaDieState : NinjaState
    {
        public NinjaDieState(NinjaController controller) : base(controller) { }

        public override void Enter()
        {
            Controller.SetAnimation("Dead");
        }
    }
}