using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMVC {
    /*
        EnemyBaseState class. Will act as a base class for all the EnemyStates.
    */
    public class EnemyBaseState
    {
        protected EnemyStateMachine enemySM;

        /*
            Constructor to set the EnemyStateMachine context.
            Parameters : 
            - _enemySM : EnemyStateMachine reference.
        */
        public EnemyBaseState(EnemyStateMachine _enemySM) {
            enemySM = _enemySM;
        }
        
        /*
            Will execute this function when Enemy enters this state.
        */
        public virtual void OnStateEnter() {}
        
        /*
            Will execute this function every frame while Enemy is in this state.
            - distance     : Distance between EnemyTank & Player Tank.
            - CHASE_RANGE  : Chase range of enemy as defined in Model.
            - ATTACK_RANGE : Attack range of enemy as defined in Model.
        */
        public virtual void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE) {}
        
        /*
            Will execute this function when Enemy exits this state.
        */
        public virtual void OnStateExit() {} 
    }

}
