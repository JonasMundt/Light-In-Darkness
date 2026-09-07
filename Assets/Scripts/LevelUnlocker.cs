using UnityEngine;

//LevelUnlock Logik -> Pc abhängig

public class LevelUnlocker : MonoBehaviour
{
    [Header("Welches Level wurde gerade beendet?")]
    public int completedLevel = 1;

    public void UnlockNextLevel()
    {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int nextLevel = completedLevel + 1;

        if (nextLevel > currentUnlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
            PlayerPrefs.Save();
        }

        Debug.Log("Freigeschaltetes Level bis: " + PlayerPrefs.GetInt("UnlockedLevel", 1));
    }
}