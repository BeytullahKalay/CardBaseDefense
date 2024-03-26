using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponState
{
    public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
        public BaseState<EState> CurrentState;

        private bool _isTransitioningState = false;

        public virtual void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            var nextSTateKey = CurrentState.GetNextState();

            if (!_isTransitioningState && nextSTateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else if (!_isTransitioningState)
                TransitionToState(nextSTateKey);
        }
        
        private void TransitionToState(EState stateKey)
        {
            _isTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[stateKey];
            CurrentState.EnterState();
            _isTransitioningState = false;
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdateState();
        }
    }
}