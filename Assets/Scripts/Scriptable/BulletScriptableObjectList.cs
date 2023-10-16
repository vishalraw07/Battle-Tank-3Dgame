using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {

    /*
        Scriptable Object List class for Bullet. Used to create Nested Scriptable Object for Bullets.
    */
    [CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddBulletScriptableObjectList")]
    public class BulletScriptableObjectList : ScriptableObject
    {
        public BulletScriptableObject[] bulletConfigs;
    }

}


