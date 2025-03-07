using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        loading,
        inGame,
        pause,
        gameOver
    }

    public GameState gameState;

    public List<GameObject> targetPrefabs;

    public float spawnRate = 5f;
    private float intervaloIncremento = 5f;
    private float contadorTiempo = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI over;
    public TextMeshProUGUI pause;

    public Button pauseButton;


    private int _score;
    private int score{
        set{
            _score = Mathf.Clamp(value,0,99999);
        }
        get{
            return _score;
        }
    }

    private int _vidas;
    private int vidas{
        set{
            _vidas = Mathf.Clamp(value,0,3);
        }
        get{
            return _vidas;
        }
    }

    private int extra = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        over.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);

        gameState = GameState.inGame;
        StartCoroutine(SpawnTarget());

        
        score = 0;
        vidas = 3;
        UpdateScore(0);
        UpdateVidas(0);

        pauseButton.onClick.AddListener(Pause);
    }

    void Update()
    {
        // Acumula el tiempo transcurrido.
        contadorTiempo += Time.deltaTime;

        // Cuando se alcance el intervalo, incrementa la velocidad y reinicia el contador.
        if (contadorTiempo >= intervaloIncremento)
        {
            spawnRate -= 0.15f;
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

    public void UpdateScore(int scoreToAdd){

        if(gameState == GameState.inGame){
            score += scoreToAdd;
            scoreText.text = "" + score;
            
            if(scoreToAdd > 0){
                extra += 1;

                if(extra >= 25){
                    UpdateVidas(-1);
                    extra = 0;
                }
            }else{
                extra = 0;
            }
        }
        
    }

    public void UpdateVidas(int perdida){

        if(gameState == GameState.inGame){
            vidas -= perdida;
            vidasText.text = "" + vidas;

            if(vidas <= 0){
                GameOver();
            }
        }
    }

    public void GameOver(){
        over.gameObject.SetActive(true);
        gameState = GameState.gameOver;
    }

    public void Pause(){
        
        
        if (gameState == GameState.pause)
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            pause.gameObject.SetActive(false);
            gameState = GameState.inGame;
        }
        else if(gameState == GameState.inGame)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            pause.gameObject.SetActive(true);
            gameState = GameState.pause;
        }
    }
}
