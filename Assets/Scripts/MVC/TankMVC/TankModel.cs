using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace TankMVC {
    /*
        Enum for different types of Tank. Used to fetch ScriptableObject properties.
    */
    public enum TankType {
        NONE = -1,
        SLOW_SPEED = 0,
        MED_SPEED = 1,
        FAST_SPEED = 2
    }
    /*
        Model class for Player Tank. Keeps track of all the properties associated with the tank.
    */
    public class TankModel
    {
        private TankController tankController;
        public float TANK_SPEED;
        public float ROTATION_SPEED;
        public int TANK_HEALTH;
        public int TANK_TOTAL_HEALTH;
        public TankType TANK_TYPE;
        public Material TANK_COLOR;

        /*
            Constructor to set attributes based on Scriptable Object.
            Parameters : 
            - tankScriptableObject : ScriptableObject from which configuration needs to be set.
        */        
        public TankModel(TankScriptableObject tankScriptableObject) {
            TANK_SPEED = tankScriptableObject.TANK_SPEED;
            ROTATION_SPEED = tankScriptableObject.ROTATION_SPEED;
            TANK_TOTAL_HEALTH = tankScriptableObject.TANK_HEALTH;
            TANK_HEALTH = tankScriptableObject.TANK_HEALTH;
            TANK_TYPE = tankScriptableObject.TANK_TYPE;
            TANK_COLOR = tankScriptableObject.TANK_MATERIAL_COLOR;
        }

        /*
            Sets the reference for tankController.
            Parameters : 
            - _tankController : TankController object reference.
        */
        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }
        
    }
}