using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMVC {
    /*
        EnemyAttackState class. Defines the functionality for ATTACK state.
    */
    public class EnemyAttackState : EnemyBaseState
    {
        
        public EnemyAttackState(EnemyStateMachine _enemySM) : base(_enemySM) {}

        /*
            Executes this function when Enemy enters ATTACK state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            // Debug.Log("ATTACK STATE ENTER.");
            enemySM.GetEnemyController().GetEnemyView().StartCoroutine(AttackPlayer());
        }

        /*
            Executes this function when Enemy stays in ATTACK state.
            Checks for State switching based on distance, CHASE_RANGE & ATTACK_RANGE.
            - distance     : Distance between EnemyTank & Player Tank.
            - CHASE_RANGE  : Chase range of enemy as defined in Model.
            - ATTACK_RANGE : Attack range of enemy as defined in Model.
        */
        public override void OnStateUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE)
        {
            base.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
            // Debug.Log("ATTACK STATE UPDATE.");
            if (distance > ATTACK_RANGE) {
                enemySM.SwitchState(EnemyState.CHASE);
            }
            SetAttackDestination();
            
        }

        /*
            Executes this function when Enemy exits ATTACK state.
        */
        public override void OnStateExit()
        {
            base.OnStateExit();
            // Debug.Log("ATTACK STATE EXIT.");
        }

        /*
            Sets Attack Destination of NavMeshAgent to player's position.
        */
        private void SetAttackDestination() {
            EnemyController _ec = enemySM.GetEnemyController();
            NavMeshAgent navAgent = _ec.GetEnemyView().GetNavMeshAgent();
            Transform playerTransform = _ec.GetPlayerTransform();
            navAgent.SetDestination(playerTransform.position);
        }

        /*
            Attacks the Player and Fires Bullets towards' Player tank.
        */
        private IEnumerator AttackPlayer() {
            EnemyController _ec = enemySM.GetEnemyController();
            Transform playerTransform = _ec.GetPlayerTransform();
            Transform enemyTransform = _ec.GetEnemyView().transform;
            while (playerTransform.gameObject.activeInHierarchy && enemyTransform.gameObject.activeInHierarchy && enemySM.GetEnemyStateEnum(enemySM.currentEnemyState) == EnemyState.ATTACK) {
                EnemyService.Instance.FireBullet(enemyTransform, _ec.GetEnemyModel().TANK_TYPE);
                yield return new WaitForSeconds(2f);
            }
        }
    }

}
