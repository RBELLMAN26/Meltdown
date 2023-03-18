using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopScoreManager : MonoBehaviour
{
    public static TopScoreManager instance;
    public List<float> scores = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        GetScoresFromPlayerPrefs();
    }

    //Gets scores if exist in playerprefs.
    void GetScoresFromPlayerPrefs()
    {
        scores.Clear();
        if(PlayerPrefs.HasKey("Scores"))
        {
            string temp = PlayerPrefs.GetString("Scores");
            foreach(string score in temp.Split(","))
            {
                scores.Add(float.Parse(score));
            }
        }
        else
        {
            PlayerPrefs.SetString("Scores", "");
        }
        scores.Sort();
        scores.Reverse();
    }

    //Saves Scores Up To 10 To PlayerPrefs
    void SaveScoresToPlayerPrefs()
    {
        scores.Sort();
        scores.Reverse();
        if(scores.Count > 10) scores.RemoveRange(10, scores.Count - 10);
        Debug.Log(string.Join(",", scores));
        PlayerPrefs.SetString("Scores", string.Join(",", scores));
    }

    //Adds new score after gameover and saves
    public void AddScore()
    {
        scores.Add(GameManager.instance.timeInGame);
        SaveScoresToPlayerPrefs();
    }
}
