using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TankMVC;
using Scriptables;

namespace EnemyMVC {

    /*
        Model class for Enemy Tank. Keeps track of all the properties associated with the enemy tank.
    */
    public class EnemyModel
    {
        private EnemyController enemyController;
        public float TANK_SPEED;
        public int TANK_HEALTH;
        public int TANK_TOTAL_HEALTH;
        public float ROTATION_SPEED;
        public TankType TANK_TYPE;
        public Material TANK_COLOR;
        public Vector3 AGENT_TARGET;
        public float ATTACK_RANGE;
        public float CHASE_RANGE;
    
        /*
            Constructor to set attributes based on Scriptable Object.
            Parameters : 
            - enemyScriptableObject : ScriptableObject from which configuration needs to be set.
        */
        public EnemyModel(EnemyScriptableObject enemyScriptableObject) {
            TANK_SPEED = enemyScriptableObject.TANK_SPEED;
            ROTATION_SPEED = enemyScriptableObject.ROTATION_SPEED;
            TANK_HEALTH = enemyScriptableObject.TANK_HEALTH;
            TANK_TOTAL_HEALTH = enemyScriptableObject.TANK_HEALTH;
            TANK_TYPE = enemyScriptableObject.TANK_TYPE;
            TANK_COLOR = enemyScriptableObject.TANK_MATERIAL_COLOR;
            AGENT_TARGET = Vector3.zero;
            ATTACK_RANGE = enemyScriptableObject.ATTACK_RANGE;
            CHASE_RANGE = enemyScriptableObject.CHASE_RANGE;
        }

        /*
            Method to set Configuration based on Scriptable Object. Used while reusing in Pool.
            Parameters : 
            - enemyScriptableObject : ScriptableObject from which configuration needs to be set.
        */
        public void SetModelConfig(EnemyScriptableObject enemyScriptableObject) {
            TANK_SPEED = enemyScriptableObject.TANK_SPEED;
            ROTATION_SPEED = enemyScriptableObject.ROTATION_SPEED;
            TANK_HEALTH = enemyScriptableObject.TANK_HEALTH;
            TANK_TYPE = enemyScriptableObject.TANK_TYPE;
            TANK_COLOR = enemyScriptableObject.TANK_MATERIAL_COLOR;
            AGENT_TARGET = Vector3.zero;
            ATTACK_RANGE = enemyScriptableObject.ATTACK_RANGE;
            CHASE_RANGE = enemyScriptableObject.CHASE_RANGE;
        }

        /*
            Sets reference to EnemyController.
            Parameters : 
            - _enemyController : EnemyController reference.
        */
        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        /*
            Returns reference to EnemyController.
        */
        public EnemyController GetEnemyController() {
            return enemyController;
        }
    }

}
