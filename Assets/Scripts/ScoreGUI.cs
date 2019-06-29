using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGUI : MonoBehaviour
{
    private ScoreController score;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI comboText;

    private void Start()
    {
        this.score = FindObjectOfType<ScoreController>();
    }

    private void OnGUI()
    {
        this.scoreText.SetText("{0}", this.score.GetScore());
        this.comboText.SetText("{0}", this.score.GetCombo());
    }
}
