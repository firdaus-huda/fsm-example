using NinjaFSM.Common;

namespace NinjaFSM.Game.Character
{
    public abstract class NinjaState : IState
    {
        protected readonly NinjaController Controller;
        protected NinjaState(NinjaController controller) => Controller = controller;
        
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}