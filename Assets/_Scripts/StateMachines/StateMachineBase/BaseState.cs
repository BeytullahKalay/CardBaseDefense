using System;

namespace WeaponState
{
    public abstract class BaseState<EState> where EState : Enum
    {
        public EState StateKey { get; private set; }
        
        
        protected EState NextState;
        
        public BaseState(EState key)
        {
            StateKey = key;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract EState GetNextState();
    }
    
    
}