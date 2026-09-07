using UnityEngine;
using UnityEngine.SceneManagement;

    //Pausen Menü

public class PauseMenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseCanvas;

    [Header("Arrow UI")] //Pfeil Sprite
    public RectTransform leftArrow;
    public RectTransform rightArrow;

    [Header("Arrow Anchor Points")] //Positionen der Pfeile
    public RectTransform[] leftAnchors;
    public RectTransform[] rightAnchors;

    private int currentIndex = 0;
    private bool isPaused = false;

    private void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
        UpdateSelection();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (!isPaused) return;

        //Bewegen nach oben mit W oder Pfeiltaste
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = leftAnchors.Length - 1;

            UpdateSelection();
            SimpleAudioManager.Instance?.PlayMenuMove();
        }
        //Bewegen nach unten mit S oder Pfeiltaste
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= leftAnchors.Length)
                currentIndex = 0;

            UpdateSelection();
            SimpleAudioManager.Instance?.PlayMenuMove();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectOption();
            SimpleAudioManager.Instance?.PlayMenuConfirm();
        }
    }

    public void PauseGame()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
        currentIndex = 0;
        UpdateSelection();
    }

    public void ResumeGame()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    private void UpdateSelection()
    {
        MoveArrowsToCurrentOption();
    }

    //Bewegung zur Anchor Location im Pausen Screen Logik
    private void MoveArrowsToCurrentOption()
    {
        if (leftAnchors == null || rightAnchors == null) return;
        if (currentIndex < 0 || currentIndex >= leftAnchors.Length) return;
        if (currentIndex >= rightAnchors.Length) return;

        if (leftArrow != null && leftAnchors[currentIndex] != null)
            leftArrow.anchoredPosition = leftAnchors[currentIndex].anchoredPosition;

        if (rightArrow != null && rightAnchors[currentIndex] != null)
            rightArrow.anchoredPosition = rightAnchors[currentIndex].anchoredPosition;
    }

    //Auswahl Optionen im Pausen Screen
    private void SelectOption()
    {
        switch (currentIndex)
        {
            case 0://Fortsetzen
                ResumeGame();
                break;

            case 1://Neustart
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case 2: //Zurück zum Hauptmenü
                Time.timeScale = 1f;
                SceneManager.LoadScene("Main_Menu");
                break;

            case 3: //Spiel verlassen
                Time.timeScale = 1f;
                Application.Quit();
                break;
        }
    }
}