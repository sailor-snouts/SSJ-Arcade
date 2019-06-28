using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreList : MonoBehaviour
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;

    public int playerPosition;

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
    }

    public void populateTable(List<Score> scores)
    {
        float templateHeight = 25f;
        int startDisplay = playerPosition < 4 ? 0 : playerPosition - 4;
        // We need to account for the players in the bottom 10
        int displayTotal = playerPosition + 5 /*Accounts for 0 indexing of lists*/ > scores.Count ? scores.Count - playerPosition + 4 : 10;
        for(int i = 0 ; i < displayTotal ; i++)
        {
            Transform entry = Instantiate(entryTemplate.transform, entryContainer.transform);
            RectTransform entryRectTransform = entry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entry.gameObject.SetActive(true);
            
            Text pos = entry.Find("pos").GetComponent<Text>();
            Text score = entry.Find("score").GetComponent<Text>();
            Text init = entry.Find("init").GetComponent<Text>();

            pos.text = (i + startDisplay).ToString();
            score.text = scores[i + startDisplay].score.ToString();
            init.text = scores[i + startDisplay].initials;

            if(i == 4)
            {
                pos.color = Color.yellow;
                score.color = Color.yellow;
                init.color = Color.yellow;
            }
        }
    } 
}
