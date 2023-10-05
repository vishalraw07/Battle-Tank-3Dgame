using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    /*
        EnemyPatrolState class. Defines the functionality for PATROL state.
    */
    public class EnemyPatrolState : EnemyBaseState
    {
        
        public EnemyPatrolState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        /*
            Executes this function when Enemy enters PATROL state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            // Debug.Log("PATROL STATE ENTER");
            enemySM.GetEnemyController().GetEnemyView().StartCoroutine(PatrolEnvironment());
        }

        /*
            Executes this function when Enemy stays in PATROL state.
            Checks for State switching based on distance, CHASE_RANGE & ATTACK_RANGE.
            - distance     : Distance between EnemyTank & Player Tank.
            - CHASE_RANGE  : Chase range of enemy as defined in Model.
            - ATTACK_RANGE : Attack range of enemy as defined in Model.
        */
        public override void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE)
        {
            base.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
            // Debug.Log("PATROL STATE UPDATE.");
            if (distance <= CHASE_RANGE) {
                enemySM.SwitchState(EnemyState.CHASE);
            }
        }

        /*
            Executes this function when Enemy exits PATROL state.
        */
        public override void OnStateExit()
        {
            base.OnStateExit();
            // Debug.Log("PATROL STATE EXIT.");
        }

        /*
            Patrols the Environment after randomly choosing an destination and changing the location after interval.
        */
        private IEnumerator PatrolEnvironment() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            Transform enemyTransform = _ec.GetEnemyView().transform;
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemySM.GetEnemyStateEnum(enemySM.currentEnemyState) == EnemyState.PATROL) {
                Vector3 NEXT_TARGET = EnemyService.Instance.GetRandomPoint(enemyTransform.position, 60f, playerTransform.position);
                navAgent.SetDestination(NEXT_TARGET);
                // Debug.Log("NEXT TARGET : " + NEXT_TARGET);
                yield return new WaitForSeconds(7.5f);
            }
        }
    }

}
