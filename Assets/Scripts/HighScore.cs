using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    TextAsset scoreFile;
    // Start is called before the first frame update
    void Start()
    {
        string[] jsonObjects = scoreFile.text.Split('\n');
        List<Score> scores = new List<Score>();
        for(int i = 0 ; i < jsonObjects.Length ; i++)
        {
            Debug.Log(jsonObjects[i]);
            Score s = JsonUtility.FromJson<Score>(jsonObjects[i]);
            scores.Add(s);
        }
        foreach(Score s in scores){
        Debug.Log(s.initials);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Score
{
    public string score;
    public string initials;
}