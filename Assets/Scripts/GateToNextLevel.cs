using UnityEngine;
using UnityEngine.SceneManagement;

//Tor in die nächste Szene Logik

public class GateToNextLevel : MonoBehaviour
{
    [Header("Level")]
    public string nextSceneName = "Level_02";
    public string spawnPointName;

    [Header("Progression")]
    public int unlockLevelIndex = 2;

    [Header("World Space Prompt")]
    public GameObject interactPrompt;

    private bool playerInRange = false;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(spawnPointName))
            {
                PlayerPrefs.SetString("SpawnPoint", spawnPointName);
            }

            // 🔥 Fortschritt speichern
            int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

            if (unlockLevelIndex > currentUnlocked)
            {
                PlayerPrefs.SetInt("UnlockedLevel", unlockLevelIndex);
                PlayerPrefs.Save();
            }

            SceneManager.LoadScene(nextSceneName);
        }
    }

    //Wenn der Player in range ist
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (interactPrompt != null)
            interactPrompt.SetActive(true);
    }

    //Wenn der Player in range ist
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }
}