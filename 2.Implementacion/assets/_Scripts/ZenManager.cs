using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZenManager : MonoBehaviour
{

    public enum GameState
    {
        inGame,
        pause
    }

    public GameState gameState;

    public List<GameObject> targetPrefabs;

    public float spawnRate = 5f;
    private float intervaloIncremento = 5f;
    private float contadorTiempo = 0f;


    public TextMeshProUGUI pause;
    public Button pauseButton;
    public Button menu;
    public Button reiniciar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        pause.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        reiniciar.gameObject.SetActive(false);
        gameState = GameState.inGame;

        
        StartCoroutine(SpawnTarget());
    }

    void Update()
    {
        // Acumula el tiempo transcurrido.
        contadorTiempo += Time.deltaTime;

        // Cuando se alcance el intervalo, incrementa la velocidad y reinicia el contador.
        if (contadorTiempo >= intervaloIncremento && spawnRate > 0)
        {
            spawnRate -= 0.5f;
            contadorTiempo = 0f;
        }
    }

    IEnumerator SpawnTarget()
    {
        while(gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    public void Pause(){
        
        
        if (gameState == GameState.pause)
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            pause.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
            reiniciar.gameObject.SetActive(false);
            gameState = GameState.inGame;
        }
        else if(gameState == GameState.inGame)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            pause.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            reiniciar.gameObject.SetActive(true);
            gameState = GameState.pause;
        }
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu(){
        gameState = GameState.inGame;
        SceneManager.LoadScene("Menu");
    }
}
