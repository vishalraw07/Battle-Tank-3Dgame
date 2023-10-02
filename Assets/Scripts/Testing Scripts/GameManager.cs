using UnityEngine;
using TMPro;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    public GameObject deathText;
    public GameObject enemyDestructionParticlePrefab;
    
    private void Start()
    {
        // Initialize the deathText to be inactive when the game starts
        if (deathText != null)
        {
            deathText.SetActive(false);
        }
    }

    public void PlayerDeath()
    {
        // Display the death text, disable player and enemies, and start the camera animation
        DeathText();
        DisablePlayerAndEnemies();
        StartCoroutine(CameraAnimation());
    }

    private void DisablePlayerAndEnemies()
    {
        // Disable the player and all enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<TankView>().enabled = false;
            player.SetActive(false);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyTankView enemyView = enemy.GetComponent<EnemyTankView>();
            if (enemyView != null)
            {
                enemyView.enabled = false;
            }
        }
    }

    IEnumerator CameraAnimation()
    {
        yield return new WaitForSeconds(2f);

        // Move the camera to a specific position and rotation, then start the enemy animation
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(-80.5f, 58.3f, -68.4f);
            mainCamera.transform.rotation = Quaternion.Euler(21.609f, 52.41f, 0);
        }

        StartCoroutine(EnemyAnimation());
    }

    IEnumerator EnemyAnimation()
    {
        yield return new WaitForSeconds(1f);

        // Destroy the enemy and instantiate destruction particles, then start the next animation
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            InstantiateEnemyDestructionParticles(enemy.transform.position);
            Destroy(enemy);
            StartCoroutine(EnemyAnimation());
        }
        else
        {
            StartCoroutine(OtherObjectsAnimation());
        }
    }

    void InstantiateEnemyDestructionParticles(Vector3 position)
    {
        // Instantiate enemy destruction particles and set a lifetime for them
        if (enemyDestructionParticlePrefab != null)
        {
            GameObject particles = Instantiate(enemyDestructionParticlePrefab, position, Quaternion.identity);
            Destroy(particles, 2.0f);
        }
    }

    IEnumerator OtherObjectsAnimation()
    {
        yield return new WaitForSeconds(1f);

        // Destroy other objects in the scene, then pause the game
        GameObject levelArt = GameObject.FindGameObjectWithTag("LevelArt");
        if (levelArt != null)
        {
            Destroy(levelArt);
            StartCoroutine(OtherObjectsAnimation());
        }
        else
        {
            Pause();
        }
    }

    public void DeathText()
    {
        // Display the death text and trigger sound effects
        if (deathText != null)
        {
            deathText.SetActive(true);
            deathText.GetComponent<TextMeshProUGUI>().text = "Congratulations, You won!";
        }
        SceneController.Instance.StopAllSounds();
        SceneController.Instance.StartSpecificSound(2);
    }
}
