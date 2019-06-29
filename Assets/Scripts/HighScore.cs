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

    // These two go out with the block in Awake().
    private int playerScore;
    private string playerID;

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

        // This next part is probably all temporary. I'd bet we should just call "recordScoreAndContinue" from the input script that makes a score
        // Added a null check for testing purposes. We can start the score scene without having a PlayerController carrying over from previous scenes
        playerID = System.DateTime.Now.ToString();
        PlayerController playerController = GetComponent<PlayerController>();
        playerScore = playerController == null ? 999 : playerController.getScore();
        //recordScoreAndContinue(new Score(playerScore, "Bobert Jenkins", playerID));
    }

    public void recordScoreAndContinue(Score s) {
        scores.Add(s);
        writeHighScoresToFile(scores);
        int playerPos = getPlayerPosition(scores, s.id);
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

    private int getPlayerPosition(List<Score> scores, string ID)
    {
        scores.Sort(new ScoreSort());
        for(int i = 0 ; i < scores.Count ; i++)
        {
            if(scores[i].id == ID)
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
    public string id;

    public Score(int value, string initials, string time)
    {
        this.score = value;
        this.initials = initials;
        this.id = time;
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