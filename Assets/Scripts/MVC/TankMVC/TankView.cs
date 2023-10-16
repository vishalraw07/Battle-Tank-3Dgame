using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankMVC {
    /*
        MonoBehaviour View Class for Player Tank. Handles all the UI, visible things related with the tank. 
    */
    public class TankView : MonoBehaviour
    {
        private TankController tankController;
         
        [SerializeField] MeshRenderer[] COLOR_MATERIALS;

        /*
            Sets the reference for tankController.
            Parameters : 
            - _tankController : TankController object reference.
        */
        public void SetTankController(TankController _tankController) {
            tankController = _tankController;
        }

        public MeshRenderer[] GetMaterialMeshes() {
            return COLOR_MATERIALS;
        }

        private void Update() {
            float horizontal = Input.GetAxisRaw("Horizontal1");
            float vertical = Input.GetAxisRaw("Vertical1");
            // Debug.Log(horizontal + " " + vertical);
            tankController.MoveTank(horizontal, vertical);
           
        }

        public Transform GetTankTransform() {
            return this.transform;
        }
    }
}
