using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {

    /*
        MonoBehaviour View class for BulletView. Handles all the visible part of the bullet.
    */
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController = null;
        
        /*
            Sets the reference for bulletController.
            Parameters : 
            - _bulletController : BulletController object reference.
        */
        public void SetBulletController(BulletController _bulletController) {
            bulletController = _bulletController;
        }

        /*
            Returns the reference to the BulletController.
        */
        public BulletController GetBulletController() {
            return bulletController;
        }

        /*
            Returns the reference to the Transform component.
        */
        public Transform GetBulletTransform() {
            return this.transform;
        }

        /*
            Handles the BulletCollision based on collidedObject.
            Parameters : 
            - collidedObject : the object which collided with Bullet.
        */
        private void OnCollisionEnter(Collision collidedObject) {
            bulletController.HandleBulletCollision(collidedObject);
        }
    }
}
