using System;

namespace ShootEmUp
{
    public interface IGameFinalizator
    {
        void Final();
    }

    public interface IGameFinalizeHandler
    {
        event Action OnFinalized;
    }
    
    public class GameFinalizator: IGameFinalizator, IGameFinalizeHandler
    {
        public event Action OnFinalized;
        
        public void Final()
        {
            OnFinalized?.Invoke();
        }
    }
}