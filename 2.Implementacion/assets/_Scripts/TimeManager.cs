using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TimeManager : MonoBehaviour
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
     private int vidas = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI over;
    public TextMeshProUGUI pause;

    public Button pauseButton;
    public Button menu;
    public Button reiniciar;

    private int _score;
    private int score{
        set{
            _score = Mathf.Clamp(value,0,99999);
        }
        get{
            return _score;
        }
    }

    public TextMeshProUGUI timerText; // Asigna un Text UI en el Inspector
    private float timeRemaining = 120f; // 2 minutos en segundos
    private bool isCounting = true;

    private int extra = 0;

    private string filePath;

    public Transform cameraTransform; // Para manipular la cámara
    private float shakeIntensity = 0.1f, shakeDuration = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Application.persistentDataPath + "/scoresTime.txt";
        over.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        reiniciar.gameObject.SetActive(false);

        Time.timeScale = 1f;
        gameState = GameState.inGame;
        StartCoroutine(SpawnTarget());
        StartCoroutine(UpdateTimer());

        
        score = 0;
        UpdateScore(0);
        
    }

    IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0 && isCounting)
        {
            // Reduce el tiempo
            timeRemaining -= Time.deltaTime;

            // Convierte los segundos en minutos y segundos
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Actualiza el texto del UI
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null; // Espera un frame
        }

        // Asegura que el tiempo llegue a 0
        timeRemaining = 0;
        timerText.text = "00:00";

        // Llamar a una función cuando termine el tiempo
        OnTimerEnd();
        GameOver();
    }

    void OnTimerEnd()
    {
        isCounting = false; // Detener el contador
    }

    void Update()
    {
        // Acumula el tiempo transcurrido.
        contadorTiempo += Time.deltaTime;
        // Cuando se alcance el intervalo, incrementa la velocidad y reinicia el contador.
        if (contadorTiempo >= intervaloIncremento)
        {
            spawnRate -= 0.10f;
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
            index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    IEnumerator ShakeCamera() {

        if(gameState == GameState.inGame){
            Vector3 originalCameraPosition = cameraTransform.position;
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration) {
                float x = Random.Range(-shakeIntensity, shakeIntensity);
                float y = Random.Range(-shakeIntensity, shakeIntensity);

                cameraTransform.position = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            cameraTransform.position = originalCameraPosition; // Restablecer la posición original
        }
        
    }

    public void UpdateVidas(int perdida){

        if(gameState == GameState.inGame){

            // Mover la cámara cuando se pierde una vida
            if (perdida > 0) {
                StartCoroutine(ShakeCamera());
                timeRemaining -= 5f;

            }else{
                timeRemaining += 5f;
            }

        }
    }

    public void destruccion() {
        shakeDuration = 2f;
        StartCoroutine(ShakeCamera());
        shakeDuration = 0.5f;
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

    public void GameOver(){
        SaveScore(score);
        over.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        reiniciar.gameObject.SetActive(true);
        gameState = GameState.gameOver;
    }

    void SaveScore(int newScore)
    {
        // Agrega la puntuación al archivo
        File.AppendAllText(filePath, newScore + "\n");
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
