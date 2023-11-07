using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using TankMVC;
using Scriptables;
using Events;
using ParticleEffects;
using GameAudio;

namespace BulletMVC {
    /*
        MonoSingleton BulletService class.
        Handles creation of bullets & operations by communicating with other Services.
    */
    public class BulletService : GenericMonoSingleton<BulletService>
    {
        [SerializeField] private BulletView BulletPrefab;
        [SerializeField] private Transform poolParentTransform;
        private GenericObjectPool<BulletView> bulletPool;
        public BulletScriptableObjectList scriptableConfigs;

        //    Generates Bullet Pool using Prefab, count & parent Transform.
                
        protected override void Awake() {
            base.Awake();
            bulletPool = new GenericObjectPool<BulletView>();
            bulletPool.GeneratePool(BulletPrefab.gameObject, 30, poolParentTransform);
        }

        /*
            Spawns the Bullet based on TankType & the transform component of the Tank. Gets Bullet & Sets MVC Attributes.
            Parameters : 
            - tankTransform : Transform of the tank spawning the bullet.
            - tankType      : type of Tank (Based on ScriptableObject)
        */
        public void SpawnBullet(Transform tankTransform, TankType tankType) {
            BulletScriptableObject bulletConfig = GetBulletConfiguration(tankType);
            BulletView bulletView = bulletPool.GetItem();
            if (bulletView.GetBulletController() == null) {
                BulletModel bulletModel = new BulletModel(bulletConfig);
                BulletController bulletController = new BulletController(bulletModel, bulletView);
                SetBulletMVCAttributes(bulletController, bulletModel, bulletView, tankTransform);
            } else {
                bulletView.GetBulletController().GetBulletModel().SetModelConfig(bulletConfig);
                SetBulletMVCAttributes(bulletView.GetBulletController(), bulletView.GetBulletController().GetBulletModel(), bulletView, tankTransform);
            }
        }

        /*
            Sets Bullet Model, View & Controller attributes by linking them with each other.
            Parameters :
            - bulletController : BulletController for this gameobject (bullet).
            - bulletModel      : BulletModel for the gameObject (bullet).
            - bulletView       : BulletView for the gameObject (bullet).
            - tankTransform    : Transform component of the tank which is spawning the bullet.
        */
        private void SetBulletMVCAttributes(BulletController bulletController, BulletModel bulletModel, BulletView bulletView, Transform tankTransform) {
            bulletView.gameObject.SetActive(true);
            bulletModel.SetBulletController(bulletController);
            bulletView.SetBulletController(bulletController);
            StartCoroutine(bulletController.FireBullet(tankTransform, bulletModel.BULLET_DISTANCE));
        }

        /*
            Destroys and Returns Bullet to the Bullet Pool. Invokes the onGameObjectDestroyed event to trigger Particle Effects.
            Parameters : 
            - bullet             : BulletController associated with the gameObject (bullet)
            - isDistanceComplete : Boolean value which signifies whether the bullet travelled complete distance or got hit by obstacle.
        */
        public void DestroyBullet(BulletController bullet, bool isDistanceComplete) {
            Vector3 finalPos = bullet.GetBulletView().transform.position;
            bullet.GetBulletView().gameObject.SetActive(false);
            bulletPool.ReturnItem(bullet.GetBulletView());
            if (!isDistanceComplete)
                EventService.Instance.InvokeParticleSystemEvent(ParticleEffectType.BULLET_EXPLOSION, finalPos);
        }

        /*
            Gets and Returns Bullet Configuration from the ScriptableObjectList based on TankType.
            Parameters : 
            - tankType : type of Tank whose bullet Configuration is required.
        */
        private BulletScriptableObject GetBulletConfiguration(TankType tankType) {
            return Array.Find(scriptableConfigs.bulletConfigs, config => config.TANK_TYPE == tankType);
        }

        /*
            Gets Damage Value of Bullet.
            Used by Other Services for updating Health Components.
        */
        public int GetBulletDamage(Collision other) {
            BulletController bulletController = other.gameObject.GetComponent<BulletView>().GetBulletController();
            return bulletController.GetBulletModel().BULLET_DAMAGE;            
        }
    }
}
