using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{


    public List<GameObject> targetPrefabs;

    public float spawnRate = 1f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI vidasText;

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
        StartCoroutine(SpawnTarget());

        score = 0;
        vidas = 3;
        UpdateScore(0);
        UpdateVidas(0);
    }

    IEnumerator SpawnTarget(){
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    public void UpdateScore(int scoreToAdd){
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

    public void UpdateVidas(int perdida){
        vidas -= perdida;
        vidasText.text = "" + vidas;
    }
}
