using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    /*
        EnemyChaseState class. Defines the functionality for CHASE state.
    */
    public class EnemyChaseState : EnemyBaseState
    {
        
        public EnemyChaseState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        /*
            Executes this function when Enemy enters CHASE state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            // Debug.Log("CHASE STATE ENTER");
        }

        /*
            Executes this function when Enemy stays in CHASE state.
            Checks for State switching based on distance, CHASE_RANGE & ATTACK_RANGE.
            - distance     : Distance between EnemyTank & Player Tank.
            - CHASE_RANGE  : Chase range of enemy as defined in Model.
            - ATTACK_RANGE : Attack range of enemy as defined in Model.
        */
        public override void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE)
        {
            base.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
            // Debug.Log("CHASE STATE UPDATE.");
            // CUSTOM IMPLEMENTATION
            if (distance > CHASE_RANGE) {
                enemySM.SwitchState(EnemyState.PATROL);
            } else if (distance > ATTACK_RANGE) {
                SetChaseDestination();
            } else {
                enemySM.SwitchState(EnemyState.ATTACK);
            }
        }

        /*
            Executes this function when Enemy exits CHASE state.
        */
        public override void OnStateExit()
        {
            base.OnStateExit();
            // Debug.Log("CHASE STATE EXIT.");
        }

        /*
            Sets Chase Destination to that of PlayerTank. Uses NavMeshAgent.
        */
        private void SetChaseDestination() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            navAgent.SetDestination(playerTransform.position);
        }
    }

}
