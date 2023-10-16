using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Generics;
using Events;
using GameAudio;

namespace GameUI {
    /*
        MonoSingleton UIService class. Handles all the UI in the Gameplay Scene.
    */
    public class UIService : GenericMonoSingleton<UIService>
    {
        private Queue<string> AchievementList = new Queue<string>();
        [SerializeField] private GameObject achievementUI;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private TextMeshProUGUI achievementText;

        /*
            Subscribes to onAchievementUnlocked & onPlayerDeath event.
        */
        private void OnEnable() {
            EventService.Instance.onAchievementUnlocked +=  AchievementUnlocked;
            EventService.Instance.onPlayerDeath += DisplayGameOverUI;
        }

        /*
            Achievement Unlocked Method. Uses Queue to handle multiple achievements at the same time.
            Method is called everytime onAchievementUnlocked event is triggered.
            Parameters : 
            - Achievement_Text : Text to be displayed on Achievement UI.
        */
        public void AchievementUnlocked(string Achievement_Text) {
            AchievementList.Enqueue(Achievement_Text);
            if (AchievementList.Count == 1)
                StartCoroutine(DisplayAchievements());
        }

        /*
            Displays Achievements until the Queue is empty. Deactivates & activates the UI accordingly.
        */
        private IEnumerator DisplayAchievements() {
            while (AchievementList.Count != 0) {
                achievementText.text = AchievementList.Peek();
                achievementUI.SetActive(true);
                AudioService.Instance.PlayAudio(GameAudio.AudioType.ACHIEVEMENT_UNLOCKED);
                yield return new WaitForSeconds(2.5f);
                achievementUI.SetActive(false);
                yield return new WaitForSeconds(1f);
                AchievementList.Dequeue();
            }
            
        }

        /*
            Restarts the Gameplay Level. Method is called when Restart Button is clicked. 
        */
        public void RestartLevel() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /*
            Goes back to Lobby scene. Method is called when Menu Button is clicked. 
        */
        public void MainMenu() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            SceneManager.LoadScene(0);
        }

        /*
            Displays the Game Over Screen. Method is called after onPlayerDeath event is triggered. 
        */
        public void DisplayGameOverUI() {
            AudioService.Instance.StopAudio(GameAudio.AudioType.LEVEL_BG);
            gameOverUI.SetActive(true);
        }

        /*
            Unsubscribes to onAchievementUnlocked & onPlayerDeath event.
        */
        private void OnDisable() {
            EventService.Instance.onAchievementUnlocked -= AchievementUnlocked;
            EventService.Instance.onPlayerDeath -= DisplayGameOverUI; 
        }
    }
}
