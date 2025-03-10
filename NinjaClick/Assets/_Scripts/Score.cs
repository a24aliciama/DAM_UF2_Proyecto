using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI scoreNormal; // Asigna un UI Text en el inspector
    public TextMeshProUGUI scoreTime; 
    private string filePathNormal;
    private string filePathTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePathNormal = Application.persistentDataPath + "/scores.txt";
        filePathTime = Application.persistentDataPath + "/scoresTime.txt";
        LoadScores();
        LoadScoresTime();
    }

    void LoadScoresTime()
    {
        if (File.Exists(filePathTime))
        {
            string[] lines = File.ReadAllLines(filePathTime);
            List<int> scores = new List<int>();

            // Convertir cada línea en un número y agregarlo a la lista
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int score))
                {
                    scores.Add(score);
                }
            }

            // Ordenar de mayor a menor
            scores.Sort((a, b) => b.CompareTo(a));

            // Si hay más de 20 puntuaciones, eliminar las más bajas
            if (scores.Count > 20)
            {
                scores = scores.GetRange(0, 20);
            }

            // Guardar la lista filtrada nuevamente en el archivo
            File.WriteAllLines(filePathTime, scores.ConvertAll(s => s.ToString()).ToArray());

            // Mostrar en la UI
            scoreTime.text = "";
            foreach (int s in scores)
            {
                scoreTime.text += s + "\n";
            }
        }
        else
        {
            scoreTime.text = "No hay puntuaciones guardadas.";
        }
    }

    void LoadScores()
    {
        if (File.Exists(filePathNormal))
        {
            string[] lines = File.ReadAllLines(filePathNormal);
            List<int> scores = new List<int>();

            // Convertir cada línea en un número y agregarlo a la lista
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int score))
                {
                    scores.Add(score);
                }
            }

            // Ordenar de mayor a menor
            scores.Sort((a, b) => b.CompareTo(a));

            // Si hay más de 20 puntuaciones, eliminar las más bajas
            if (scores.Count > 20)
            {
                scores = scores.GetRange(0, 20);
            }

            // Guardar la lista filtrada nuevamente en el archivo
            File.WriteAllLines(filePathNormal, scores.ConvertAll(s => s.ToString()).ToArray());

            // Mostrar en la UI
            scoreNormal.text = "";
            foreach (int s in scores)
            {
                scoreNormal.text += s + "\n";
            }
        }
        else
        {
            scoreNormal.text = "No hay puntuaciones guardadas.";
        }
    }
}

