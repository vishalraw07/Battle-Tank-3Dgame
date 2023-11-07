using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankMVC;
using EnemyMVC;

namespace Scriptables {
    /*
        Scriptable Object class for Enemy Tank. Used to create scriptable objects for different types of Enemy Tanks.
    */
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AddEnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float TANK_SPEED;
        public float ROTATION_SPEED;
        public int TANK_HEALTH;
        public TankType TANK_TYPE;
        public Material TANK_MATERIAL_COLOR;
        public float CHASE_RANGE;
        public float ATTACK_RANGE;
            
    }
}
