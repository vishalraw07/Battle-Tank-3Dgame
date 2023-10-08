using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMVC;

namespace Scriptables {

    /*
        Scriptable Object class for Bullet. Used to create scriptable objects for different types of bullets.
    */
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddBulletScriptableObject")]
    public class BulletScriptableObject : ScriptableObject
    {
        public int BULLET_DAMAGE;
        public float BULLET_SPEED;
        public float BULLET_DISTANCE;     
        public TankType TANK_TYPE;   
    }

}

