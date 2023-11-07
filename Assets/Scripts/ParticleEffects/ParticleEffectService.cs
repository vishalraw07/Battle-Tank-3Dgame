using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Events;

namespace ParticleEffects {

    /*
        Enum for different types of Particle Effects.
    */
    public enum ParticleEffectType {
        BULLET_EXPLOSION,
        TANK_EXPLOSION
    }

    /*
        MonoSingleton ParticleEffectService class.
        Handles Pool of ParticleEffects & positions them accordingly.
    */
    public class ParticleEffectService : GenericMonoSingleton<ParticleEffectService>
    {
        [SerializeField] private ParticleSystem TankExplosionPrefab;
        [SerializeField] private Transform bulletPETransform;
        [SerializeField] private Transform tankPETransform;
        [SerializeField] private ParticleSystem BulletExplosionPrefab;

        private GenericObjectPool<ParticleSystem> tankExplosionPEPool;
        private GenericObjectPool<ParticleSystem> bulletExplosionPEPool;
        private EventService eventService;

        /*
            Creates Event Service & Generates Pool for Tank Explosion & Bullet Explosion Particle Effects.
        */
        protected override void Awake() {
            base.Awake();
            eventService = new EventService();
            tankExplosionPEPool = new GenericObjectPool<ParticleSystem>();
            bulletExplosionPEPool = new GenericObjectPool<ParticleSystem>();
            tankExplosionPEPool.GeneratePool(TankExplosionPrefab.gameObject, 30, tankPETransform);
            bulletExplosionPEPool.GeneratePool(BulletExplosionPrefab.gameObject, 50, bulletPETransform);
        }

        /*
            Subscribes to onGameObjectDestroyed Event to trigger ParticleEffects.
        */
        private void OnEnable() {
            EventService.Instance.onGameObjectDestroyed += DisplayParticleEffect;
        }

        /*
            Displays Particle Effect based on type of ParticleEffect & the spawning position.
            Parameters : 
            - particleEffectType : Type of ParticleEffect (BULLET / TANK)
            - position           : Position where ParticleEffect needs to be displayed.
        */
        public void DisplayParticleEffect(ParticleEffectType particleEffectType, Vector3 position) {
            if (particleEffectType == ParticleEffectType.BULLET_EXPLOSION) {
                ParticleSystem bulletPE = bulletExplosionPEPool.GetItem();
                bulletPE.gameObject.transform.position = position;
                bulletPE.gameObject.SetActive(true);
                bulletPE.Play();
                bulletExplosionPEPool.ReturnItem(bulletPE);

            } else if (particleEffectType == ParticleEffectType.TANK_EXPLOSION) {
                ParticleSystem tankPE = tankExplosionPEPool.GetItem();
                tankPE.gameObject.transform.position = position;
                tankPE.gameObject.SetActive(true);
                tankPE.Play();
                tankExplosionPEPool.ReturnItem(tankPE);
            }
        }

        //    Unsubscribes to onGameObjectDestroyed Event to trigger ParticleEffects.
        
        private void OnDisable() {
            EventService.Instance.onGameObjectDestroyed -= DisplayParticleEffect;
        }
    }

}

