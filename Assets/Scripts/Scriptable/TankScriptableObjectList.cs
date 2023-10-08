using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {
    /*
        Scriptable Object List class for Player tank. Used to create Nested Scriptable Object for Player Tanks.
    */
    [CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddTankScriptableObjectList")]
    public class TankScriptableObjectList : ScriptableObject
    {
        public TankScriptableObject[] tankConfigs;
    }

}
