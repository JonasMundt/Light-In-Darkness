using UnityEngine;

//Startet die Audio

public class LevelAudioStarter : MonoBehaviour
{
    public enum LevelType
    {
        LevelBasic,
        Level4,
        Level6
    }

    public LevelType levelType;
    public SimpleAudioManager audioManagerPrefab;

    void Start()
    {
        SimpleAudioManager.EnsureInstance(audioManagerPrefab);

        if (SimpleAudioManager.Instance == null) return;

        switch (levelType)
        {
            case LevelType.LevelBasic:
                SimpleAudioManager.Instance.PlayLevelBasic();
                break;

            case LevelType.Level4:
                SimpleAudioManager.Instance.PlayLevel4();
                break;

            case LevelType.Level6:
                SimpleAudioManager.Instance.PlayLevel6Ambient();
                break;
        }
    }
}