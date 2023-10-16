using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HealthServices;

namespace EnemyMVC {
    /*
        MonoBehaviour View class for Enemy Tank. All the UI and visible stuff is handled in this class.
    */
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController = null;
        private NavMeshAgent navMeshAgent;
        [SerializeField] HealthBar healthBar;
        [SerializeField] MeshRenderer[] COLOR_MATERIALS;
        
        /*
            Sets reference to EnemyController.
            Parameters : 
            - _enemyController : EnemyController reference.
        */
        public void SetEnemyController(EnemyController _enemyController) {
            enemyController = _enemyController;
        }

        private void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();    
        }

        private void Update() {
            enemyController.UpdateEnemyState();
        }

       
        public HealthBar GetHealthBar() {
            return healthBar;
        }

       
        public MeshRenderer[] GetMaterialMeshes() {
            return COLOR_MATERIALS;
        }

        /*
            Handles the collision function when any gameObject collides with Tank.
            Parameters : 
            - collidedObject : the object by which tank collided.
        */
        private void OnCollisionEnter(Collision collidedObject) {
            enemyController.HandleEnemyCollision(collidedObject);
        }

        public NavMeshAgent GetNavMeshAgent() {
            return this.navMeshAgent;
        }
        
        public Transform GetEnemyTransform() {
            return this.transform;
        }

        public EnemyController GetEnemyController() {
            return enemyController;
        }
    }

}
