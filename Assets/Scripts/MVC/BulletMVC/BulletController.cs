using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletMVC {

    /*
        Controller class for Bullet.
        Used to handle all logic & functionality of the Bullet Gameobject.
    */
    public class BulletController
    {
        private BulletModel bulletModel;
        private BulletView bulletView;

        /*
            Constructor to set BulletModel & BulletView attributes.
            Parameters :
            - _bulletModel : BulletModel object.
            - _bulletView  : BulletView object.
        */
        public BulletController(BulletModel _bulletModel, BulletView _bulletView) {
            bulletModel = _bulletModel;
            bulletView = _bulletView;
        }

        /*
            Fires the Bullet in a certain direction based on tank's Transform and bullet distance.
            Destroys the bullet when distance is complete.
            Parameters : 
            - tankTransform : Transform component of tank who's firing the bullet.
            - distance      : Bullet Distance as defined in the Scriptable Object.
        */
        public IEnumerator FireBullet(Transform tankTransform, float distance) {
            Vector3 spawnPosition = tankTransform.position;
            spawnPosition.y = 1f;
            bulletView.transform.position = spawnPosition;
            bulletView.transform.rotation = tankTransform.rotation;
            bulletView.transform.forward = tankTransform.forward;
            
            Vector3 bulletForward = bulletView.transform.forward;
            Vector3 targetPosition = spawnPosition + bulletForward * distance;
            targetPosition.y = 1f;

            while (bulletView.gameObject.activeInHierarchy && Vector3.Distance(bulletView.transform.position, targetPosition) > 0.5f) {
                bulletView.transform.Translate(bulletForward * Time.deltaTime * bulletModel.BULLET_SPEED, Space.World);
                yield return new WaitForEndOfFrame();
            }
            // DESTROY BULLET
            if (bulletView.gameObject.activeInHierarchy)
                BulletService.Instance.DestroyBullet(this, true);
        }

        //    Returns reference to the BulletModel for the bullet.
        
        public BulletModel GetBulletModel() {
            return bulletModel;
        }
        
        //    Returns reference to the BulletView for the bullet.
        
        public BulletView GetBulletView() {
            return bulletView;
        }

        /*
            Handles Collision of Bullet with any gameObject.
            Parameters : 
            - collidedObject : Object with which bullet collided.
        */
        public void HandleBulletCollision(Collision collidedObject) {
            BulletService.Instance.DestroyBullet(this, false);
        }
        
    }

}
