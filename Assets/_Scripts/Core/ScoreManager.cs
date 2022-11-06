using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore
{
    public string scoreName;
    public int score;
}

public class ScoreManager : MonoBehaviour
{
    List<HighScore> scoreList;
    // Start is called before the first frame update
    void Start()
    {
        List<HighScore> scoreList = new List<HighScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addNewHighScore(string newScoreName, int newScore)
    {
        scoreList.Add(new HighScore { scoreName = newScoreName, score = newScore });
    }
}
