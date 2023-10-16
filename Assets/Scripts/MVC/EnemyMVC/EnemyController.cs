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
        
        private Transform enemyTransform;
        private Transform playerTransform;
        private HealthBar healthBar;
        
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
            Sets EnemyController attributes including initial Position, and NavMeshAgent parameters.
        */
        public void SetEnemyControllerAttributes() {
            navAgent = enemyView.gameObject.GetComponent<NavMeshAgent>();
            enemyTransform = enemyView.GetEnemyTransform();
            SetNavAgentParameters();
            Vector3 enemyPos = enemyTransform.position;
            enemyPos = EnemyService.Instance.GetRandomPoint(enemyPos, 60f, playerTransform.position);
            enemyTransform.position = enemyPos;
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

        public EnemyModel GetEnemyModel() {
            return enemyModel;
        }

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

        public Transform GetPlayerTransform() {
            return playerTransform;
        }
    }

}
