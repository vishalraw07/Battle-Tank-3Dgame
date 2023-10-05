using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HealthServices {
    /*
        HealthBar Monobehaviour class. Attached to Health Bar gameobject of every tank. Handles all the operations specific to Health Bar.
    */
    public class HealthBar : MonoBehaviour
    {
        private Transform MainCameraRig;
        [SerializeField] private Image fill;
        
        private void Start()
        {
            MainCameraRig = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<Transform>();
            fill.fillAmount = 1f;
        }

        /*
            Updates the Transform of the HealthBar and points it towards the camera.
        */
        private void LateUpdate()
        {
            transform.LookAt(MainCameraRig);
        }

        /*
            Update the Fill value of the HealthBar. 0 means empty, 1 means filled.
            Parameters : 
            - currentHealth : Current Health of the Tank.
            - totalHealth   : Total Health of the Tank.
        */
        public void UpdateFill(float currentHealth, float totalHealth) {
            fill.fillAmount = currentHealth / totalHealth;
        }


    }

}
