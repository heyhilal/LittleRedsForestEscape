using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource ambienceSource;
    public AudioSource sfxSource;
    public AudioSource footstepSource;
    public AudioSource uiSource;

    [Header("Background Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    [Header("UI Sounds")]
    public AudioClip buttonClickSFX;

    [Header("Gameplay SFX")]
    public AudioClip crystalCollectSFX;
    public AudioClip damageSFX;
    public AudioClip jumpSFX;
    public AudioClip levelCompleteSFX;
    public AudioClip gameOverSFX;

    [Header("Movement SFX")]
    public AudioClip footstepSFX;

    [Header("Water / Fall SFX")]
    public AudioClip fallingScreamSFX;
    public AudioClip waterSplashSFX;

    [Header("Landing SFX")]
    public AudioClip landingSFX;

    [Header("Wolf SFX")]
    public AudioClip wolfHowlSFX;
    public AudioClip wolfChaseSFX;

    [Header("Volume Balance")]
    [Range(0f, 1f)] public float footstepVolume = 0.30f;
    [Range(0f, 1f)] public float crystalVolume = 0.45f;
    private float currentSFXVolume = 1f;

[Header("Explosion SFX")]
public AudioClip explosionSFX;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        PlaySceneMusic();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic();
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    void PlaySceneMusic()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("Music Source boş!");
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        AudioClip targetClip = null;

        if (sceneName == "MainMenu")
        {
            targetClip = mainMenuMusic;
        }
        else
        {
            targetClip = gameplayMusic;
        }

        if (targetClip == null)
        {
            Debug.LogWarning(sceneName + " for not found music!");
            return;
        }

      if (musicSource.clip == targetClip)
{
    if (!musicSource.isPlaying)
        musicSource.Play();

    return;
}

        musicSource.Stop();
        musicSource.clip = targetClip;
        musicSource.loop = true;
        musicSource.mute = false;
        musicSource.Play();

    
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        currentSFXVolume = volume;

        if (sfxSource != null)
            sfxSource.volume = volume;

        if (uiSource != null)
            uiSource.volume = volume;

        if (footstepSource != null)
            footstepSource.volume = 1f;
    }

    public void SetAmbienceVolume(float volume)
    {
        if (ambienceSource != null)
            ambienceSource.volume = volume;
    }

    public void SetUIVolume(float volume)
    {
        if (uiSource != null)
            uiSource.volume = volume;
    }

    public void PlayButtonClick()
    {
        if (buttonClickSFX != null && uiSource != null)
            uiSource.PlayOneShot(buttonClickSFX);
    }

    public void PlayCrystalCollect()
{
    if (crystalCollectSFX != null && sfxSource != null)
        sfxSource.PlayOneShot(
            crystalCollectSFX,
            crystalVolume * currentSFXVolume
        );
}

    public void PlayDamageSound()
    {
        if (damageSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(damageSFX);
    }

    public void PlayJumpSound()
    {
        if (jumpSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(jumpSFX);
    }

    public void PlayFootstepSound()
    {
        if (footstepSFX != null && footstepSource != null)
            footstepSource.PlayOneShot(footstepSFX, footstepVolume * currentSFXVolume);
    }

public void PlayLevelCompleteSound()
{
   

    if (levelCompleteSFX != null && sfxSource != null)
    {
        sfxSource.PlayOneShot(levelCompleteSFX);
    }
    else
    {
        Debug.LogWarning("Level Complete SFX or SFX Source null!");
    }
}

    public void PlayGameOverSound()
    {
        if (gameOverSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(gameOverSFX);
    }

    public void PlayFallingScream()
    {
        if (fallingScreamSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(fallingScreamSFX);
    }

    public void PlayWaterSplash()
    {
        if (waterSplashSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(waterSplashSFX);
    }

    public void PlayLandingSound()
    {
        if (landingSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(landingSFX);
    }

    public void PlayWolfHowl()
    {
        if (wolfHowlSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(wolfHowlSFX);
    }

    public void PlayWolfChaseSound()
    {
        if (wolfChaseSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(wolfChaseSFX);
    }

    public void PlayExplosionSound()
{
    if (explosionSFX != null && sfxSource != null)
        sfxSource.PlayOneShot(explosionSFX);
}
}