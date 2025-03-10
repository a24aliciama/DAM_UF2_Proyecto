using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> targetPrefabs;

    public float spawnRate = 5f;
    private float intervaloIncremento = 5f;
    private float contadorTiempo = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
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
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    public void NormalGame(){
        SceneManager.LoadScene("Normal Game");
    }

    public void ZenGame(){
        SceneManager.LoadScene("Zen Game");
    }

    public void TimeGame(){
        SceneManager.LoadScene("Time Game");
    }

    public void ScoreNormal(){
        SceneManager.LoadScene("Score Normal");
    }

    public void Exit(){
        Application.Quit();
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    }
}
