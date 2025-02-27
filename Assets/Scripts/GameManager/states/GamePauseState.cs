using ATG.StateMachine;

namespace ShootEmUp
{
    public sealed class GamePauseState: Statement
    {
        public GamePauseState(IStateSwitcher sw) : base(sw)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}