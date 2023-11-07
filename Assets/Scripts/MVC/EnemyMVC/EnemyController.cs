using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HealthServices;

namespace EnemyMVC {
    /*
        Controller class for Enemy Tank. 
        Handles all the logic & functionality for the Enemy Tank Gameobject.
    */
    public class EnemyController
    {
        private EnemyModel enemyModel;
        private EnemyView enemyView;
        // REFERENCES FROM ENEMY VIEW , MODEL & SERVICE
        private NavMeshAgent navAgent;
        private Transform enemyTransform;
        private Transform playerTransform;
        private HealthBar healthBar;
        private EnemyStateMachine enemySM;

        /*
            Constructor to set EnemyModel & EnemyView attributes. Also sets reference to StateMachine & HealthBar.
            Parameters :
            - _enemyModel : EnemyModel object.
            - _enemyView  : EnemyView object.
        */
        public EnemyController(EnemyModel _enemyModel, EnemyView _enemyView) {
            enemyModel = _enemyModel;
            enemyView = _enemyView;
            enemySM = new EnemyStateMachine();
            healthBar = enemyView.GetHealthBar();
        }

        /*
            Sets EnemyStateMachine reference.
            Parameters : 
            - _enemySM : EnemyStateMachine object reference.
        */
        public void SetEnemySM(EnemyStateMachine _enemySM) {
            enemySM = _enemySM;
        }

        //    Returns reference to EnemyStateMachine object.
        
        public EnemyStateMachine GetEnemySM() {
            return enemySM;
        }

        //    Sets EnemyController attributes including initial Position, and NavMeshAgent parameters.
        
        public void SetEnemyControllerAttributes() {
            navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
            enemyTransform = enemyView.GetEnemyTransform();
            SetNavAgentParameters();
            Vector3 enemyPos = enemyTransform.position;
            enemyPos = EnemyService.Instance.GetRandomPoint(enemyPos, 60f, playerTransform.position);
            enemyTransform.position = enemyPos;
        }

        //    Set NavMeshAgent component's parameters. Depend on EnemyModel.
        
        private void SetNavAgentParameters() {
            NavMeshAgent agent = navAgent;
            agent.speed = enemyModel.TANK_SPEED;
            agent.angularSpeed = enemyModel.ROTATION_SPEED;
            navAgent = agent;
        }

        /*
            Sets the reference of Transform of PlayerTank.
            Parameters : 
            - _playerTransform : Transform compoenent of player Tank.
        */
        public void SetPlayerTransform(Transform _playerTransform) {
            playerTransform = _playerTransform;
        }

        /*
            Sets Tank Color based on Material from EnemyScriptableObject.
            Parameters : 
            - TANK_COLOR : Material for the color of the Enemy.
        */
        public void SetTankColor(Material TANK_COLOR) {
            MeshRenderer[] colorMaterials = enemyView.GetMaterialMeshes();
            for (int i = 0; i < colorMaterials.Length; i++) {
                Material[] materials = colorMaterials[i].materials;
                materials[0] = TANK_COLOR;
                colorMaterials[i].materials = materials;
            }
        }

        //    Updates the State of Enemy and calls EnemyStateMachine's Update function.
        
        public void UpdateEnemyState() {
            float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
            float CHASE_RANGE = enemyModel.CHASE_RANGE;
            float ATTACK_RANGE = enemyModel.ATTACK_RANGE;
            enemySM.ESMUpdate(distance, CHASE_RANGE, ATTACK_RANGE);
        }

        //    Returns reference to the EnemyModel for the enemy tank.
        
        public EnemyModel GetEnemyModel() {
            return enemyModel;
        }

        //    Returns reference to the EnemyView for the enemy tank.
        
        public EnemyView GetEnemyView() {
            return enemyView;
        }

        /*
            Handles Collision of EnemyTank with any gameObject.
            Parameters : 
            - collidedObject : Object with which Enemytank collided.
        */
        public void HandleEnemyCollision(Collision collidedObject) {

            if (collidedObject.gameObject.CompareTag("Bullet")) {
                int BULLET_DAMAGE = EnemyService.Instance.GetBulletDamage(collidedObject);
                enemyModel.TANK_HEALTH = Mathf.Max(0, enemyModel.TANK_HEALTH - BULLET_DAMAGE);
                healthBar.UpdateFill(enemyModel.TANK_HEALTH, enemyModel.TANK_TOTAL_HEALTH);
                if (enemyModel.TANK_HEALTH == 0)
                    EnemyService.Instance.DestroyTank(this);
            }
        }

        //    Returns reference to the Transform compoenent attached to the enemy.
        
        public Transform GetPlayerTransform() {
            return playerTransform;
        }
    }

}
