using UnityEngine;

public class Effects : MonoBehaviour
{
  
    public static Effects instance;
    private AudioSource audioSource;

    public AudioClip corteSound;  // Sonido para Good
    public AudioClip errorSound;  // Sonido para Bad

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye al cambiar de escena
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para reproducir el sonido correspondiente
    public void PlaySoundEffect(string tag)
    {
        if (audioSource != null)
        {
            if (tag == "Good")
            {
                audioSource.PlayOneShot(corteSound);
            }
            else if (tag == "Bad")
            {
                audioSource.PlayOneShot(errorSound);
            }
        }
    }
}
