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
    private GameObject highScoreDisplayObject;
    [SerializeField]
    private AnyKeyChangeScene anyKeyChangeSceneObject;
    [SerializeField]
    private GameObject initialsInputObject;
    
    private List<Score> scores = new List<Score>();
    private int playerScore;
    private string playerID;

    void Awake()
    {
        playerID = System.DateTime.Now.ToString();
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
        playerScore = playerController == null ? 999 : playerController.getScore();

        addNewScore(playerScore, "Hey look, I'm in the middle!", playerID);
        writeHighScoresToFile(scores);
        int playerPos = getPlayerPosition(scores, playerID);
        displayScores(playerPos);
        
    }

    private void displayScores(int playerPosition)
    {
        highScoreDisplayObject.SetActive(true);
        HighScoreList listScript = highScoreDisplayObject.GetComponent<HighScoreList>();
        listScript.playerPosition = playerPosition;
        listScript.populateTable(scores);
        anyKeyChangeSceneObject.gameObject.SetActive(true);
    }

    private void addNewScore(int score, string initials, string playerID)
    {
        scores.Add(new Score(score, initials, playerID));
    }

    private int getPlayerPosition(List<Score> scores, string ID)
    {
        scores.Sort(new ScoreSort());
        for(int i = 0 ; i < scores.Count ; i++)
        {
            if(scores[i].time == ID)
            {
                return i;
            }
        }
        return scores.Count;
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
    public string time;

    public Score(int value, string initials, string time)
    {
        this.score = value;
        this.initials = initials;
        this.time = time;
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