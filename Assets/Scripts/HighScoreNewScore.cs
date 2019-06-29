using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreNewScore : MonoBehaviour
{
    HighScore highScoreScript;
    string initials;

    void Awake()
    {
        highScoreScript = FindObjectOfType<HighScore>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Escape) && Input.anyKeyDown)
        {
            // Replace this with the real score
            completeScoreAndContinue(new Score(1001, "Charles", System.DateTime.Now.ToString()));
        }
    }

    void completeScoreAndContinue(Score playersScore) {
        // Uncomment this before enabling!
        //playersScore.initials = initials;
        highScoreScript.recordScoreAndContinue(playersScore);
        Destroy(gameObject);
    }
}
