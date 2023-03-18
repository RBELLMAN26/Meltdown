using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUpdate : MonoBehaviour
{
    TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    //Changes float to correct time for text
    public void SetTime(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)(timer / 60) % 60;
        int hours = (int)(timer / 3600) % 24;
        timerText.text = $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }

}
