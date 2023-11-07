using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using BulletMVC;
using Scriptables;
using Events;
using ParticleEffects;

namespace TankMVC {
    /*
        MonoSingleton TankService class.
        Handles creation of player tank & operations by communicating with other Services.
    */
    public class TankService : GenericMonoSingleton<TankService>
    {
        [SerializeField] private TankView tankPrefab;
        public TankScriptableObjectList scriptableConfigs;
        
        protected override void Awake() {
            base.Awake();
            CreatePlayerTank();
        }

        
        //    Creates a player tank with random configuration & sets MVC Attributes.
        
        public void CreatePlayerTank() {
            int randomIndex = Random.Range(0, scriptableConfigs.tankConfigs.Length);
            TankModel tankModel = new TankModel(scriptableConfigs.tankConfigs[randomIndex]);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            SetTankMVCAttributes(tankController, tankModel, tankView);
        }

        /*
            Sets Tank Model, View & Controller attributes by linking them with each other.
            Parameters :
            - tankController : TankController for this gameobject (tank).
            - tankModel      : TankModel for the gameObject (tank).
            - tankView       : TankView for the gameObject (tank).
        */
        private void SetTankMVCAttributes(TankController tankController, TankModel tankModel, TankView tankView) {
            tankController.SetTankColor(tankModel.TANK_COLOR);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);
        }

        /*
            Fires the Bullet and invokes the onPlayerFiredBullet Event. Also calls the BulletService for spawning bullet.
            Parameters :
            - tankTransform : Transform of the tank which is requesting the bullet.
            - tankType      : Type of tank, which will decide the bullet Configuration.
        */
        public void FireBullet(Transform tankTransform, TankType tankType) {
            EventService.Instance.InvokePlayerFiredEvent();
            BulletService.Instance.SpawnBullet(tankTransform, tankType);
        }

        /*
            Gets the Damage from the collidedObject, which is a Bullet. 
            As BulletService is used, function is called from TankService & not TankController.
            Parameters : 
            - collidedObject : the object Tank Collided with (Bullet).
        */
        public int GetBulletDamage(Collision collidedObject) {
            return BulletService.Instance.GetBulletDamage(collidedObject);
        }

        /*
            Destroys the Tank & invokes onGameObjectDestroyed & onPlayerDeath events.
            Parameters :
            - tankController : Reference to the TankController object.
        */
        public void DestroyTank(TankController tankController) {
            tankController.GetTankView().gameObject.SetActive(false);
            EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.TANK_EXPLOSION, tankController.GetTankView().transform.position);
            EventService.Instance.InvokePlayerDeathEvent();
        }
    }
}