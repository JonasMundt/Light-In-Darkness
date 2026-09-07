using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//MainMenu Steuerung

public class MainMenuController : MonoBehaviour
{
    [Header("Level UI Images (oben nach unten)")]
    public Image[] levelImages;

    [Header("Arrow UI")]
    public RectTransform leftArrow;
    public RectTransform rightArrow;

    [Header("Level Scene Names")]
    public string[] sceneNames;

    [Header("Visual Settings")]
    public Color selectedColor = Color.white;
    public Color unlockedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    public Color lockedColor = new Color(0.35f, 0.35f, 0.35f, 0.7f);

    private int currentLevelIndex = 0;
    private int unlockedLevel = 1; // Level 1 ist immer frei

    private void Start()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        currentLevelIndex = Mathf.Clamp(currentLevelIndex, 0, levelImages.Length - 1);

        UpdateMenuVisuals();

        SimpleAudioManager.EnsureInstance();

        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayMainMenu();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PlaySelectedLevel();
        }
    }

    //MoveHoch im Menü
    public void MoveUp()
    {
        currentLevelIndex--;

        if (currentLevelIndex < 0)
            currentLevelIndex = levelImages.Length - 1;

        UpdateMenuVisuals();

        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayMenuMove();
        }
    }

    //MoveRunter im Menü
    public void MoveDown()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelImages.Length)
            currentLevelIndex = 0;

        UpdateMenuVisuals();

        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayMenuMove();
        }
    }

    //Level wird gestartet
    public void PlaySelectedLevel()
    {
        int selectedLevelNumber = currentLevelIndex + 1;

        if (selectedLevelNumber > unlockedLevel)
        {
            Debug.Log("Dieses Level ist noch gesperrt.");
            return;
        }

        if (sceneNames != null && currentLevelIndex < sceneNames.Length)
        {
            if (SimpleAudioManager.Instance != null)
            {
                SimpleAudioManager.Instance.PlayMenuConfirm();
            }

            SceneManager.LoadScene(sceneNames[currentLevelIndex]);
        }
    }

    //Level freischalten -> Pc abhängig
    private void UpdateMenuVisuals()
    {
        for (int i = 0; i < levelImages.Length; i++)
        {
            if (levelImages[i] == null)
                continue;

            int levelNumber = i + 1;
            bool isUnlocked = levelNumber <= unlockedLevel;
            bool isSelected = i == currentLevelIndex;

            if (!isUnlocked)
            {
                levelImages[i].color = lockedColor;
            }
            else if (isSelected)
            {
                levelImages[i].color = selectedColor;
            }
            else
            {
                levelImages[i].color = unlockedColor;
            }
        }
        MoveArrowsToSelectedLevel();
    }

    //Bewegung der Pfeile
    private void MoveArrowsToSelectedLevel()
    {
        if (levelImages == null || currentLevelIndex < 0 || currentLevelIndex >= levelImages.Length)
            return;

        RectTransform selectedRect = levelImages[currentLevelIndex].rectTransform;

        if (selectedRect == null)
            return;

        Vector2 selectedPos = selectedRect.anchoredPosition;

        if (leftArrow != null)
        {
            Vector2 leftPos = leftArrow.anchoredPosition;
            leftArrow.anchoredPosition = new Vector2(leftPos.x, selectedPos.y);
        }

        if (rightArrow != null)
        {
            Vector2 rightPos = rightArrow.anchoredPosition;
            rightArrow.anchoredPosition = new Vector2(rightPos.x, selectedPos.y);
        }
    }
}