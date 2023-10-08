using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMVC {

    /*
        Enum for Different EnemyStates.
    */
    public enum EnemyState {
        ATTACK,
        PATROL,
        CHASE,
        NONE
    }

    /*
        Class to implement State Machine for different Enemy States. Keeps track of Current State and state switching.
    */
    public class EnemyStateMachine
    {
        private EnemyController enemyController = null;
        private EnemyAttackState attackState;
        private EnemyChaseState chaseState;
        private EnemyPatrolState patrolState;
        public EnemyBaseState currentEnemyState = null;

        /*
            Constructor to create references for all the different EnemyStates.
        */
        public EnemyStateMachine() {
            attackState = new EnemyAttackState(this);
            chaseState = new EnemyChaseState(this);
            patrolState = new EnemyPatrolState(this);
            // SwitchState(EnemyState.PATROL);
        }

        /*
            Sets reference to EnemyController & switches the EnemyState to patrol.
            Parameters : 
            - _enemyController : Reference to EnemyController object.
        */
        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
            SwitchState(EnemyState.PATROL);
        }

        /*
            Returns the reference to EnemyController attached with StateMachine.
        */
        public EnemyController GetEnemyController() {
            return enemyController;
        }

        /*
            Used to switch between different EnemyStates using the Enum.
            Parameters : 
            - enemyState : EnemyState to switch to. (PATROL, ATTACK, CHASE, NONE) 
        */
        public void SwitchState(EnemyState enemyState) {
            if (GetEnemyStateEnum(currentEnemyState) == enemyState)
                return;
            if (currentEnemyState != null)
                currentEnemyState.OnStateExit();
            currentEnemyState = GetEnemyBaseState(enemyState);
            currentEnemyState.OnStateEnter();
        }

        /*
            Returns EnemyState Enum based on EnemyBaseState.
            Parameters : 
            - enemyBaseState : object type of baseState.
        */
        public EnemyState GetEnemyStateEnum(EnemyBaseState enemyBaseState) {
            if (enemyBaseState == attackState) {
                return EnemyState.ATTACK;
            } else if (enemyBaseState == chaseState) {
                return EnemyState.CHASE;
            } else if (enemyBaseState == patrolState) {
                return EnemyState.PATROL;
            } else {
                return EnemyState.NONE;
            }
        }

        /*
            Returns BaseState of the enemy to set the EnemyBaseState.
            Parameters : 
            - enemyState : EnemyState enum value.
        */
        private EnemyBaseState GetEnemyBaseState(EnemyState enemyState) {
            if (enemyState == EnemyState.ATTACK) {
                return attackState;
            } else if (enemyState == EnemyState.CHASE) {
                return chaseState;
            } else if (enemyState == EnemyState.PATROL) {
                return patrolState;
            } else {
                return null;
            }
        }

        /*
            Functionality on what happens in the update frame based on current state.
            Parameters : 
            - distance     : Distance between EnemyTank & Player Tank.
            - CHASE_RANGE  : Chase range of enemy as defined in Model.
            - ATTACK_RANGE : Attack range of enemy as defined in Model. 
        */
        public void ESMUpdate(float distance, float CHASE_RANGE, float ATTACK_RANGE) {
            if (currentEnemyState != null)
                currentEnemyState.OnStateUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
        }
    }

}
