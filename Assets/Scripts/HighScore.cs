using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class HighScore : MonoBehaviour
{
    
    
    List<Score> scores = new List<Score>();
    private ScoreSort sort = new ScoreSort();
    int playerScore;

    [SerializeField]
    TextAsset scoreFile;

    void Start()
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
        Debug.Log("Is player high scoring?: " + isPlayerHighScoring(scores, playerScore));
    }

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


    private void writeHighScores(List<Score> newScores)
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