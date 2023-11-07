using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {
    /*
        Scriptable Object List class for Enemy Tank. Used to create Nested Scriptable Object for Enemy Tanks.
    */
    [CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/AddEnemyScriptableObjectList")]
    public class EnemyScriptableObjectList : ScriptableObject
    {
        public EnemyScriptableObject[] enemyConfigs;
    }

}

