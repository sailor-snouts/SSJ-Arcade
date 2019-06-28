using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TextAsset scoreFile;
    [SerializeField]
    private GameObject canvas;
    
    private List<Score> scores = new List<Score>();
    private ScoreSort sort = new ScoreSort();
    private int playerScore;

    void Awake()
    {
        // JsonUtility can't read lists, so we've got to make each line its own json object to process
        string[] jsonObjects = scoreFile.text.Split('\n');
        for(int i = 0 ; i < jsonObjects.Length ; i++)
        {
            string json = jsonObjects[i];
            // AppendLine used later to re-write the json file adds a \n to the end of the file. This breaks before attempting to add an empty score to the list.
            if(json.Length <= 1) break;
            Score s = JsonUtility.FromJson<Score>(jsonObjects[i]);
            scores.Add(s);
        }

        // Added a null check for testing purposes. We can start the score scene without having a PlayerController carrying over from previous scenes
        PlayerController playerController = GetComponent<PlayerController>();
        playerScore = playerController == null ? 0 : playerController.getScore();
        if(isPlayerHighScoring(scores, playerScore))
        {
            // Logic for showing & actiavting the initials input goes here. On that input block, it should clean itself up and call display scores
        }else
        {
            displayScores();
        }
    }

    #region display high scores
    private void displayScores()
    {
        canvas.gameObject.SetActive(true);
        canvas.gameObject.GetComponent<HighScoreList>().populateTable(scores);
    }

    #endregion

    public static Boolean isPlayerHighScoring(List<Score> toCheck, int myScore)
    {
        toCheck.Sort(new ScoreSort());
        return myScore > toCheck[toCheck.Count -1].score;
    }

    public void addNewScore(int score, string initials)
    {
        scores.Sort(sort);
        // Remove the lowest score before adding
        scores.RemoveAt(scores.Count - 1);
        scores.Add(new Score(score, initials));
    }

    #region output to file
    private void writeHighScoresToFile(List<Score> newScores)
    {
        StringBuilder json = new StringBuilder();
        foreach(Score s in newScores)
            {
            json.AppendLine(JsonUtility.ToJson(s));
            }
        Debug.Log(json.ToString());
       
        // False here overwrites the file instead of appending to it
        StreamWriter writer = new StreamWriter("Assets/Scores/scores.json", false);
        writer.WriteLine(json.ToString());
        writer.Close();
    }
    #endregion
}

[Serializable]
public class Score
{
    public int score;
    public string initials;

    public Score(int value, string initials)
    {
        this.score = value;
        this.initials = initials;
    }
}

public class ScoreSort : IComparer<Score>
{
    int IComparer<Score>.Compare(Score x, Score y)
    {
        // We want it in descending order so the highest score is on top
        if (x.score < y.score)
            return 1;
        if (x.score > y.score)
            return -1;
        else
            return 0;
    }
}