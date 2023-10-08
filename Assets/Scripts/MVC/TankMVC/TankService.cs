using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Scriptables;
 
 

namespace TankMVC {
    /*
        MonoSingleton TankService class. Handles creation of player tank & operations by communicating with other Services.
    */
    public class TankService : GenericMonoSingleton<TankService>
    {
        [SerializeField] private TankView tankPrefab;
        public TankScriptableObjectList scriptableConfigs;
        
        protected override void Awake() {
            base.Awake();
            CreatePlayerTank();
        }

        /*
            Creates a player tank with random configuration & sets MVC Attributes.
        */
        public void CreatePlayerTank() {
            int randomIndex = Random.Range(0, scriptableConfigs.tankConfigs.Length);
            TankModel tankModel = new TankModel(scriptableConfigs.tankConfigs[randomIndex]);
            TankView tankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankController tankController = new TankController(tankModel, tankView);
            SetTankMVCAttributes(tankController, tankModel, tankView);
        }

        /*
            Sets Tank Model, View & Controller attributes by linking them with each other.
            Parameters :
            - tankController : TankController for this gameobject (tank).
            - tankModel      : TankModel for the gameObject (tank).
            - tankView       : TankView for the gameObject (tank).
        */
        private void SetTankMVCAttributes(TankController tankController, TankModel tankModel, TankView tankView) {
            tankController.SetTankColor(tankModel.TANK_COLOR);
            tankModel.SetTankController(tankController);
            tankView.SetTankController(tankController);
        }


    }
}