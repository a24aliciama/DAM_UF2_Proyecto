using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public AudioClip menuMusic;  // Para Menu y Score
    public AudioClip gameMusic;  // Para Normal Game y Time Game
    public AudioClip zenMusic;   // Para Zen Game

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye al cambiar de escena
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded; // Detecta cambio de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name); // Música inicial
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name); // Cambia música según la escena
    }

    void PlayMusicForScene(string sceneName)
    {
        AudioClip newClip = null;

        if (sceneName == "Normal Game" || sceneName == "Time Game") 
        {
            newClip = gameMusic;
        }
        else if (sceneName == "Zen Game") 
        {
            newClip = zenMusic;
        }
        else // Para Menu y Score
        {
            newClip = menuMusic;
        }

        ChangeMusic(newClip);
    }

    void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip) // Evita reiniciar la misma música
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
