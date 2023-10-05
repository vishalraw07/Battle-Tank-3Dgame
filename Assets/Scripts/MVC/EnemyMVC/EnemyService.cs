using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Generics;
using TankMVC;
using BulletMVC;
using Scriptables;
using Events;
using ParticleEffects;

namespace EnemyMVC {

    /*
        MonoSingleton EnemyService class. Handles creation of enemy tanks & operations by communicating with other Services.
    */
    public class EnemyService : GenericMonoSingleton<EnemyService>
    {
        [SerializeField] private EnemyView enemyPrefab;
        [SerializeField] private Transform enemyPoolParentTransform;
        private GenericObjectPool<EnemyView> enemyPool;
        
        public EnemyScriptableObjectList scriptableConfigs;
        private Transform playerTank;
        
        /*
            Initialize Player Tank and Generate Enemy Pool.
        */
        protected override void Awake() {
            base.Awake();
            playerTank = GameObject.FindGameObjectWithTag("Player").transform;
            enemyPool = new GenericObjectPool<EnemyView>();
            enemyPool.GeneratePool(enemyPrefab.gameObject, 10, enemyPoolParentTransform);
            // MAKE A COROUTINE WHICH WILL KEEP ON SPAWNING NEW ENEMIES
            StartCoroutine(SpawnEnemiesAtInterval());
        }

        /*
            Coroutine which spawns Enemies at Interval of 15seconds.
            Keeps on spawning until Player Dies.
        */
        private IEnumerator SpawnEnemiesAtInterval() {
            for (int i = 0; i < 5; i++)
                SpawnEnemy();
            while (playerTank.gameObject.activeInHierarchy) {
                yield return new WaitForSeconds(15f);
                SpawnEnemy();
            }
        }

        /*
            Spawns a new Enemy with Random Configs after enabling an object from EnemyPool.
        */
        private void SpawnEnemy() {
            // Debug.Log(scriptableConfigs.enemyConfigs);
            int randomIndex = UnityEngine.Random.Range(0, scriptableConfigs.enemyConfigs.Length);
            EnemyView enemyView = enemyPool.GetItem();
            if (enemyView.GetEnemyController() == null) {
                EnemyModel enemyModel = new EnemyModel(scriptableConfigs.enemyConfigs[randomIndex]);
                EnemyController enemyController = new EnemyController(enemyModel, enemyView);
                EnemyStateMachine enemySM = enemyController.GetEnemySM();
                SetEnemyMVCAttributes(enemyController, enemyModel, enemyView, enemySM);
            } else {
                enemyView.GetEnemyController().GetEnemyModel().SetModelConfig(scriptableConfigs.enemyConfigs[randomIndex]);
                SetEnemyMVCAttributes(enemyView.GetEnemyController(), enemyView.GetEnemyController().GetEnemyModel(), enemyView, enemyView.GetEnemyController().GetEnemySM());
            }
            
        }

        /*
            Sets Enemy Model, View, Controller & State Machine attributes by linking them with each other.
            Parameters :
            - enemyController : EnemyController for this gameobject (enemy).
            - enemyModel      : EnemyModel for the gameObject (enemy).
            - enemyView       : EnemyView for the gameObject (enemy).
            - enemySM         : EnemyStateMachine for the gameObject(enemy)
        */
        private void SetEnemyMVCAttributes(EnemyController enemyController, EnemyModel enemyModel, EnemyView enemyView, EnemyStateMachine enemySM) {
            enemyView.gameObject.SetActive(true);
            enemyController.SetPlayerTransform(playerTank);
            enemyController.SetTankColor(enemyModel.TANK_COLOR);
            enemyModel.SetEnemyController(enemyController);
            enemyView.SetEnemyController(enemyController);
            enemySM.SetEnemyController(enemyController);
            enemyController.SetEnemySM(enemySM);
            enemyController.SetEnemyControllerAttributes();
        }

        /*
            Fires the Bullet & calls the BulletService for spawning bullet.
            Parameters :
            - tankTransform : Transform of the enemy tank which is requesting the bullet.
            - tankType      : Type of tank, which will decide the bullet Configuration.
        */
        public void FireBullet(Transform tankTransform, TankType tankType) {
            BulletService.Instance.SpawnBullet(tankTransform, tankType);
        }

        /*
            Gets the Damage from the collidedObject, which is a Bullet. 
            As BulletService is used, function is called from EnemyService & not EnemyController.
            Parameters : 
            - collidedObject : the object Enemy Collided with (Bullet).
        */
        public int GetBulletDamage(Collision collidedObject) {
            return BulletService.Instance.GetBulletDamage(collidedObject);
        }

        /*
            Destroys the Enemy Tank, Returns it to EnemyPool & invokes onGameObjectDestroyed & onEnemyDeath events.
            Parameters :
            - enemyController : Reference to the EnemyController object.
        */
        public void DestroyTank(EnemyController enemyController) {
            enemyController.GetEnemyView().GetHealthBar().UpdateFill(enemyController.GetEnemyModel().TANK_TOTAL_HEALTH, enemyController.GetEnemyModel().TANK_TOTAL_HEALTH);
            enemyController.GetEnemyView().gameObject.SetActive(false);
            enemyPool.ReturnItem(enemyController.GetEnemyView());
            UnityEditor.MPE.EventService.Instance.InvokeEnemyDeathEvent();
            UnityEditor.MPE.EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.TANK_EXPLOSION, enemyController.GetEnemyView().transform.position);
        }

        /*
            Returns a Random Point which is away from Player.
            Used to set destination for NavMeshAgent & EnemySpawning.
            Parameters :
            - center : Current Position.
            - range  : Range of RandomPoint.
            - playerPosition : Position of Player Tank.
        */
        public Vector3 GetRandomPoint(Vector3 center, float range, Vector3 playerPosition) {
            Vector3 result = Vector3.zero;
            while (result == Vector3.zero || Vector3.Distance(result, playerPosition) < 35f) {
                Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                }
            }
            return result;
        }
    }

}
