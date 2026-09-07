using UnityEngine;

//Audio Manager

public class SimpleAudioManager : MonoBehaviour
{
    public static SimpleAudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource uiSource;

    [Header("Volumes")]
    [Range(0f, 1f)] public float musicVolume = 0.3f;
    [Range(0f, 1f)] public float sfxVolume = 0.6f;
    [Range(0f, 1f)] public float uiVolume = 0.8f;

    [Header("Music / Ambience")]
    public AudioClip MainMenu;
    public AudioClip LevelBasic;
    public AudioClip Level4;
    public AudioClip Ambiente_lvl6;

    [Header("Level 6")]
    public AudioClip Light_lvl6;

    [Header("SFX")]
    public AudioClip Death;
    public AudioClip Respawn;
    public AudioClip LoudThunder;

    [Header("UI")]
    public AudioClip menuMove;
    public AudioClip menuConfirm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupSourcesIfMissing();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetupSourcesIfMissing()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.loop = true;
            musicSource.spatialBlend = 0f;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.spatialBlend = 0f;
        }

        if (uiSource == null)
        {
            uiSource = gameObject.AddComponent<AudioSource>();
            uiSource.playOnAwake = false;
            uiSource.loop = false;
            uiSource.spatialBlend = 0f;
        }
    }

    public static void EnsureInstance(SimpleAudioManager prefab = null)
    {
        if (Instance != null) return;

        if (prefab != null)
        {
            Instance = Instantiate(prefab);
            return;
        }

        GameObject audioObj = new GameObject("AudioManager");
        Instance = audioObj.AddComponent<SimpleAudioManager>();
    }

    public void ApplyVolumes()
    {
        if (musicSource != null) musicSource.volume = musicVolume;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlayMainMenu()
    {
        PlayMusic(MainMenu, true);
    }

    public void PlayLevelBasic()
    {
        PlayMusic(LevelBasic, true);
    }

    public void PlayLevel4()
    {
        PlayMusic(Level4, true);
    }

    public void PlayLevel6Ambient()
    {
        PlayMusic(Ambiente_lvl6, true);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayDeath()
    {
        PlaySFX(Death);
    }

    public void PlayRespawn()
    {
        PlaySFX(Respawn);
    }

    public void PlayThunder()
    {
        PlaySFX(LoudThunder);
    }

    public void PlayLightLevel6()
    {
        PlaySFX(Light_lvl6);
    }

    public void PlayMenuMove()
    {
        if (menuMove == null || uiSource == null) return;
        uiSource.PlayOneShot(menuMove, uiVolume);
    }

    public void PlayMenuConfirm()
    {
        if (menuConfirm == null || uiSource == null) return;
        uiSource.PlayOneShot(menuConfirm, uiVolume);
    }
}