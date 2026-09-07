using UnityEngine;

//Startet die Audio am Anfang

public class MainMenuAudioStarter : MonoBehaviour
{
    void Start()
    {
        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayMainMenu();
        }
    }
}