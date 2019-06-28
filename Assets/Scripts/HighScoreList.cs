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

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
    }

    public void populateTable(List<Score> scores)
    {
        float templateHeight = 25f;
        for(int i = 0; i < scores.Count ; i++)
        {
            Transform entry = Instantiate(entryTemplate.transform, entryContainer.transform);
            RectTransform entryRectTransform = entry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entry.gameObject.SetActive(true);
            
            
            entry.Find("score").GetComponent<Text>().text = scores[i].score.ToString();
            entry.Find("init").GetComponent<Text>().text = scores[i].initials;
        }
    } 
}
