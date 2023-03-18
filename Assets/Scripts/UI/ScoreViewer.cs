using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    public Transform scoreText;

    //Clears all Scores and Reloads from scores List
    public void LoadScores()
    {
        foreach(TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            Destroy(text.gameObject);
        }
        foreach(float score in TopScoreManager.instance.scores)
        {
            Transform temp = Instantiate(scoreText, transform);
            if(temp.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tempText))
            {
                tempText.text = SetTime(score);
            }
        }
    }

    //Returns Correct Time To Display
    string SetTime(float timer)
    {
       int seconds = (int)timer % 60;
       int minutes = (int)(timer / 60) % 60;
       int hours = (int)(timer / 3600) % 24;
       return $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
}
