using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Generics;
using GameAudio;

namespace GameUI {
    /*
        MonoSingleton LobbyService class. Handles all the operations of Lobby Scene.
    */
    public class LobbyService : GenericMonoSingleton<LobbyService>
    {
        [SerializeField] private GameObject MainCamera;

        private void Start() {
            StartCoroutine(AnimateCamera());
        }

        /*
            Animates the Camera to have an oscillating effect.
        */
        private IEnumerator AnimateCamera() {
            while (true) {
                while (MainCamera.transform.eulerAngles.y <= 90) {
                    Vector3 eulerAngles = MainCamera.transform.eulerAngles;
                    eulerAngles.y += Time.deltaTime * 10;
                    MainCamera.transform.eulerAngles = eulerAngles;
                    yield return new WaitForEndOfFrame();
                }
                while (MainCamera.transform.eulerAngles.y >= 10) {
                    Vector3 eulerAngles = MainCamera.transform.eulerAngles;
                    eulerAngles.y -= Time.deltaTime * 10;
                    MainCamera.transform.eulerAngles = eulerAngles;
                    yield return new WaitForEndOfFrame();
                }
                yield return null;
            }
        }

        /*
            Play the Game. Load the next Scene. Method is called when Play Button is clicked.
        */
        public void Play() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            AudioService.Instance.PlayAudio(GameAudio.AudioType.LEVEL_BG);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /*
            Quits the Application. Method is called when Quit Button is clicked.
        */
        public void Quit() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            Application.Quit();
        }
    }

}
