using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace BulletMVC {
    /*
        Model class for Bullet. Keeps track of all the properties associated with the Bullet.
    */
    public class BulletModel
    {
        private BulletController bulletController;
        public int BULLET_DAMAGE;
        public float BULLET_SPEED;
        public float BULLET_DISTANCE;

        /*
            Constructor to set attributes based on Scriptable Object.
            Parameters : 
            - bulletScriptableObject : ScriptableObject from which configuration needs to be set.
        */        
        public BulletModel(BulletScriptableObject bulletScriptableObject) {
            BULLET_DAMAGE = bulletScriptableObject.BULLET_DAMAGE;
            BULLET_SPEED = bulletScriptableObject.BULLET_SPEED;
            BULLET_DISTANCE = bulletScriptableObject.BULLET_DISTANCE;
        }

        /*
            Method to set Configuration based on Scriptable Object. Used while reusing in Pool.
            Parameters : 
            - bulletScriptableObject : ScriptableObject from which configuration needs to be set.
        */
        public void SetModelConfig(BulletScriptableObject bulletScriptableObject) {
            BULLET_DAMAGE = bulletScriptableObject.BULLET_DAMAGE;
            BULLET_SPEED = bulletScriptableObject.BULLET_SPEED;
            BULLET_DISTANCE = bulletScriptableObject.BULLET_DISTANCE;
        }

        /*
            Returns the reference to the BulletController.
        */
        public BulletController GetBulletController() {
            return bulletController;
        }

        /*
            Sets the reference for bulletController.
            Parameters : 
            - _bulletController : BulletController object reference.
        */
        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

    }

}
