using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace TankMVC {
    /*
        Controller class for Player Tank. 
        Handles all the logic & functionality for the PlayerTank Gameobject.
    */
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;

        // REFERENCES FROM VIEW
        private Transform tankTransform;
         
        /*
            Constructor to set TankModel & TankView attributes. Also sets reference to HealthBar & Transform.
            Parameters :
            - _tankModel : TankModel object.
            - _tankView  : TankView object.
        */        
        public TankController(TankModel _tankModel, TankView _tankView) {
            tankModel = _tankModel;
            tankView = _tankView;
             
            tankTransform = tankView.GetTankTransform();
        }

        /*
            Sets Tank Color based on Material from TankScriptableObject.
            Parameters : 
            - TANK_COLOR : Material for the color of the tank.
        */
        public void SetTankColor(Material TANK_COLOR) {
            MeshRenderer[] colorMaterials = tankView.GetMaterialMeshes();
            for (int i = 0; i < colorMaterials.Length; i++) {
                Material[] materials = colorMaterials[i].materials;
                materials[0] = TANK_COLOR;
                colorMaterials[i].materials = materials;
            }
        }

        /*
            Handles Tank Movement based on horizontal & vertical Inputs.
            Parameters : 
            - horizontal : Rotational Input.
            - vertical   : Acceleration / Brake Input.
        */
        public void MoveTank(float horizontal, float vertical) {
            SetTankVelocity(vertical);
            SetTankRotation(horizontal, vertical);
        }

        /*
            Sets Tank Rtation based on horizontal & vertical Inputs.
            Parameters : 
            - horizontal : Rotational Input.
            - vertical   : Acceleration / Brake Input.
        */
        private void SetTankRotation(float horizontal, float vertical)
        {
            if (horizontal != 0 && vertical != 0) {
                tankTransform.Rotate(new Vector3(0, horizontal * vertical * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);    
            } else if (vertical == 0) {
                tankTransform.Rotate(new Vector3(0, horizontal * tankModel.ROTATION_SPEED * Time.deltaTime, 0), Space.World);    
            }
        }

        /*
            Sets Tank Forward / Backward movement based on vertical Input.
            Parameters : 
            - vertical   : Acceleration / Brake Input.
        */
        private void SetTankVelocity(float vertical)
        {
            tankTransform.Translate(vertical * tankTransform.forward.normalized * tankModel.TANK_SPEED * Time.deltaTime, Space.World);
        }

        public TankModel GetTankModel() {
            return tankModel;
        }
 
        public TankView GetTankView() {
            return tankView;
        }
    }

}
